using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque.Db;
using Estoque.Entidades;
using Estoque.Dtos;

namespace Estoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TituloContasController : ControllerBase
    {
        private readonly EstoqueContext _context;

        public TituloContasController(EstoqueContext context)
        {
            _context = context;
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
                else
                {
                    throw;
                }
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
            titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoParcial;
            if (titulo.Saldo == 0)
            {
                titulo.Situacao = Dtos.Enums.TituloContasSituacao.LiquidadoIntegral;
            }
            _context.TituloContas.Update(titulo);
            await _context.SaveChangesAsync();
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