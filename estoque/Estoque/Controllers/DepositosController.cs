using System;
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
    public class DepositosController : ControllerBase
    {
        private readonly EstoqueContext _context;

        public DepositosController(EstoqueContext context)
        {
            _context = context;
        }

        // GET: api/Depositos
        [HttpGet]
        public IEnumerable<Deposito> GetDeposito()
        {
            return _context.Deposito;
        }

        // GET: api/Depositos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeposito([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deposito = await _context.Deposito.FindAsync(id);

            if (deposito == null)
            {
                return NotFound();
            }

            return Ok(deposito);
        }

        // PUT: api/Depositos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeposito([FromRoute] Guid id, [FromBody] Deposito deposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deposito.Id)
            {
                return BadRequest();
            }

            _context.Entry(deposito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepositoExists(id))
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

        // POST: api/Depositos
        [HttpPost]
        public async Task<IActionResult> PostDeposito([FromBody] Deposito deposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Deposito.Add(deposito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeposito", new { id = deposito.Id }, deposito);
        }

        // DELETE: api/Depositos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeposito([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deposito = await _context.Deposito.FindAsync(id);
            if (deposito == null)
            {
                return NotFound();
            }

            _context.Deposito.Remove(deposito);
            await _context.SaveChangesAsync();

            return Ok(deposito);
        }

        private bool DepositoExists(Guid id)
        {
            return _context.Deposito.Any(e => e.Id == id);
        }
    }
}