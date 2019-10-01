﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public MovimentosController(EstoqueContext context)
        {
            _context = context;
        }

        // GET: api/Movimentos
        [HttpGet]
        public IEnumerable<Movimento> GetMovimento()
        {
            return _context.Movimento;
        }

        // GET: api/Movimentos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimento([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movimento = await _context.Movimento.FindAsync(id);

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
                if (!MovimentoExists(id))
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

        // POST: api/Movimentos
        [HttpPost]
        public async Task<IActionResult> PostMovimento([FromBody] Movimento movimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var inv = _context.Inventario.FirstOrDefault(x => x.produtoId == movimento.ProdutoId);
            var invQnt = inv.quantidade;
            if (inv == null && movimento.Tipo == Tipo.eAquisicao || movimento.Tipo == Tipo.eDevolucao || movimento.Tipo == Tipo.eFabricação)
            {
                _context.Inventario.Add(new Inventario
                {
                    id = new Guid(),
                    produtoId = movimento.ProdutoId,
                    quantidade = invQnt + movimento.Quantidade
                });
            }
            else if (inv == null && movimento.Tipo == Tipo.sConsumo || movimento.Tipo == Tipo.sDevolucao || movimento.Tipo == Tipo.sOrdem || movimento.Tipo == Tipo.sVenda)
            {
                if (invQnt > movimento.Quantidade)
                {
                    _context.Inventario.Add(new Inventario
                    {
                        id = new Guid(),
                        produtoId = movimento.ProdutoId,
                        quantidade = invQnt - movimento.Quantidade
                    });
                }
                throw new Exception("Não há produtos suficientes no inventario para ser retirado");
            }
            else
            {
                throw new Exception("Não há produto em estoque");
            }

            _context.Movimento.Add(movimento);
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

            var movimento = await _context.Movimento.FindAsync(id);
            if (movimento == null)
            {
                return NotFound();
            }

            _context.Movimento.Remove(movimento);
            await _context.SaveChangesAsync();

            return Ok(movimento);
        }

        private bool MovimentoExists(Guid id)
        {
            return _context.Movimento.Any(e => e.Id == id);
        }
    }
}