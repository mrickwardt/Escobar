using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque.Db;
using Estoque.Entidades;

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
            return _context.Movimentacoes;
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

            if ((int) input.Tipo >= 2 && produtoVinculado.Quantidade < input.Quantidade)
            {
                return BadRequest(
                    "A quantidade do produto " + produtoVinculado.Nome + " é menor que a solicitada");
            }

            var movimento = _mapper.Map<Movimento>(input);
            movimento.ProdutoVinculado = produtoVinculado;
            movimento.ProdutoId = produtoVinculado.Id;
            if ((int) input.Tipo >= 2)
            {
                // Saída
                produtoVinculado.Quantidade -= input.Quantidade;
            }
            else
            {
                // Entrada
                produtoVinculado.Quantidade += input.Quantidade;
            }

            var movimentacoesAnteriores = _context.Movimentacoes
                .Where(m => m.ProdutoId == input.ProdutoVinculadoId && ((int) m.Tipo == 3 || (int) m.Tipo == 4 || (int) m.Tipo == 5))
                .Select(m => new
                {
                    m.Quantidade,
                    m.Valor
                }).ToList();
            var totalQuantidade = movimentacoesAnteriores.Sum(x => x.Quantidade);
            var totalValor = movimentacoesAnteriores.Sum(x => x.Valor * x.Quantidade);
            if((int) input.Tipo == 3 || (int) input.Tipo == 4 || (int) input.Tipo == 5)
            {
                totalQuantidade += input.Quantidade;
                totalValor += input.Valor * input.Quantidade;
            }

            produtoVinculado.PrecoMedio = totalValor != 0 || totalQuantidade != 0 ? totalValor / totalQuantidade : 0;
            _context.Produtos.Update(produtoVinculado);
            await _context.Movimentacoes.AddAsync(movimento);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetMovimento", new { id = movimento.Id }, movimento);
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
        [Route("ProdutoVinculado/{id}")]
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