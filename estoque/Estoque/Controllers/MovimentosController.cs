using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque.Db;
using Estoque.Entidades;
using Estoque.Shared;

namespace Estoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentosController : ControllerBase
    {
        private readonly EstoqueContext _context;
        private readonly IMapper _mapper;

        public MovimentosController(EstoqueContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Movimentos
        [HttpGet]
        public IEnumerable<Movimento> GetMovimento()
        {
            return _context.Movimentacoes.Where(m => !m.IsCongelado);
        }

        [HttpGet("MovimentosCongelados")]
        public IEnumerable<Movimento> GetMovimentoCongelado()
        {
            return _context.Movimentacoes.Where(m => m.IsCongelado);
        }

        // GET: api/Movimentos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimento([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movimento = await _context.Movimentacoes.FindAsync(id);

            if (movimento == null)
            {
                return NotFound();
            }

            return Ok(movimento);
        }

        // PUT: api/Movimentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimento([FromRoute] Guid id, [FromBody] Movimento movimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movimento.Id)
            {
                return BadRequest();
            }

            _context.Entry(movimento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimentoExiste(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Movimentos
        [HttpPost]
        public async Task<IActionResult> PostMovimento([FromBody] MovimentoInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produtoVinculado = _context.Produtos.Find(input.ProdutoVinculadoId);

            if (produtoVinculado == null)
            {
                return BadRequest("Produto vinculado não encontrado");
            }

            if (IsTipoSaida(input) && produtoVinculado.Quantidade < input.Quantidade)
            {
                return BadRequest(
                    "A quantidade do produto " + produtoVinculado.Nome + " é menor que a solicitada");
            }

            if (IsTipoSaida(input))
            {
                // Saida
                var movimentoVenda = new MovimentoVenda(_context);
                await movimentoVenda.VendaProduto(produtoVinculado, input.Quantidade, input.Valor);
                return Ok();
            }
            else if (input.MovimentacaoTipo != MovimentacaoTipo.cancelamento)
            {
                // Entrada
                produtoVinculado.Quantidade += input.Quantidade;
                _context.Produtos.Update(produtoVinculado);
                return Ok();
            }
            // Cancelamento
            return BadRequest("Acesse o método de TituloContas/CancelarPedido");
        }

        private static bool IsTipoSaida(MovimentoInput input)
        {
            return input.MovimentacaoTipo == MovimentacaoTipo.sConsumo ||
                            input.MovimentacaoTipo == MovimentacaoTipo.sDevolucao ||
                            input.MovimentacaoTipo == MovimentacaoTipo.sOrdem ||
                            input.MovimentacaoTipo == MovimentacaoTipo.sVenda;
        }

        // DELETE: api/Movimentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimento([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movimento = await _context.Movimentacoes.FindAsync(id);
            if (movimento == null)
            {
                return NotFound();
            }

            _context.Movimentacoes.Remove(movimento);
            await _context.SaveChangesAsync();

            return Ok(movimento);
        }

        // GET: api/Movimentos/5
        [HttpGet]
        [Route("Produto/{id}")]
        public async Task<List<Movimento>> GetMovimentoPorProdutoVinculado([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            var listaMovimentacao = _context.Movimentacoes.Where(m => m.ProdutoId == id).ToListAsync();

            return await listaMovimentacao;
        }

        private bool MovimentoExiste(Guid id)
        {
            return _context.Movimentacoes.Any(e => e.Id == id);
        }


    }
}