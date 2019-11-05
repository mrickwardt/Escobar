using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque.Db;
using Estoque.Entidades;
using Estoque.Dtos;
using Estoque.Shared;
using AutoMapper;

namespace Estoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TituloContasController : ControllerBase
    {
        private readonly EstoqueContext _context;
        private readonly IMapper _mapper;
        public TituloContasController(EstoqueContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TituloContas
        [HttpGet]
        public IEnumerable<TituloContas> GetTituloContas()
        {
            return _context.TituloContas;
        }

        // GET: api/TituloContas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTituloContas([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tituloContas = await _context.TituloContas.FindAsync(id);

            if (tituloContas == null)
            {
                return NotFound();
            }

            return Ok(tituloContas);
        }

        // PUT: api/TituloContas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTituloContas([FromRoute] Guid id, [FromBody] TituloContas tituloContas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tituloContas.Id)
            {
                return BadRequest();
            }

            _context.Entry(tituloContas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TituloContasExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/TituloContas
        [HttpPost]
        public async Task<IActionResult> PostTituloContas([FromBody] TituloContas tituloContas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tituloContas.Situacao = Dtos.Enums.TituloContasSituacao.Aberto;
            var movimento = new Movimento
            {
                Data = DateTime.Now,
                Documento = new Documento {Tipo = TipoDocumento.Fiscal},
                Natureza = Natureza.dev,
                MovimentacaoTipo = MovimentacaoTipo.sVenda,
                TituloContaId = tituloContas.Id
                // movimento.ProdutoId
                // movimento.Quantidade
                // movimento.Evento
            };

            _context.Movimentacoes.Add(movimento);
            _context.TituloContas.Add(tituloContas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTituloContas", new { id = tituloContas.Id }, tituloContas);
        }

        // DELETE: api/TituloContas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTituloContas([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tituloContas = await _context.TituloContas.FindAsync(id);
            if (tituloContas == null)
            {
                return NotFound();
            }

            _context.TituloContas.Remove(tituloContas);
            await _context.SaveChangesAsync();

            return Ok(tituloContas);
        }

        private bool TituloContasExists(Guid id)
        {
            return _context.TituloContas.Any(e => e.Id == id);
        }
        public async Task<IActionResult> LiquidacaoParcial(TituloLiquidacaoParcialInput input)
        {
            var produto = await _context.Produtos.FindAsync(input.ProdutoId);
            if (produto == null)
                return BadRequest("Produto não encontrado!");
            var titulo = _context.TituloContas.FirstOrDefault(t => t.ProdutoId == input.ProdutoId);
            if (titulo == null)
                return BadRequest("Título não encontrado!");
            if (titulo.Saldo < input.Valor)
                return BadRequest("Valor maior que o do titulo!");

            var movimentoProduto = new MovimentoProduto(_context);
            await movimentoProduto.LiquidacaoParcial(produto, titulo, input.Valor);
            return Ok(produto);
        }
        public async Task<IActionResult> LiquidacaoIntegral(Guid produtoId)
        {
            var produto = await _context.Produtos.FindAsync(produtoId);
            if (produto == null)
                return BadRequest("Produto não encontrado!");
            var titulo = _context.TituloContas.FirstOrDefault(t => t.ProdutoId == produtoId);
            if (titulo == null)
                return BadRequest("Título não encontrado!");
            var movimentoProduto = new MovimentoProduto(_context);
            await movimentoProduto.LiquidacaoIntegral(produto, titulo);
            return Ok(titulo);
        }
        //2.2 Um titulo pode ser liquidado por subtituição: Por exemplo, um título é
        //aberto para a compra em questão, então o cliente decide por pagar com cartão 
        //de crédito.Neste momento o títulos é substituido por outro título, cujo sacado 
        //passa a ser a operadora de cartão de crédito.Essa movimentação também precisa ser 
        //mapeada de forma a integrar os valor contabilmente
        public async Task<IActionResult> LiquidacaoPorSubstituicao(TituloLiquidacaoSubstituicaoInput input)
        {
            var titulo = _context.TituloContas.Find(input.TituloId);
            if (titulo == null)
            {
                return BadRequest("Titulo não encontrado!");
            }
            if (titulo.Saldo < input.Valor)
            {
                return BadRequest("Valor maior que o do titulo!");
            }
            titulo.Saldo -= input.Valor;
            _context.TituloContas.Update(titulo);
            await _context.SaveChangesAsync();
            return Ok(titulo);

        }
    }
}