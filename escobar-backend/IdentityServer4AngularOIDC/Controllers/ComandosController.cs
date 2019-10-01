using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandosController : ControllerBase
    {
        private readonly UserContext _context;

        public ComandosController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Comandos
        [HttpGet]
        public IEnumerable<Comando> GetComandos()
        {
            return _context.Comandos;
        }

        // GET: api/Comandos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComando([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comando = await _context.Comandos.FindAsync(id);

            if (comando == null)
            {
                return NotFound();
            }

            return Ok(comando);
        }

        // PUT: api/Comandos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComando([FromRoute] Guid id, [FromBody] Comando comando)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comando.Id)
            {
                return BadRequest();
            }

            _context.Entry(comando).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandoExists(id))
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

        // POST: api/Comandos
        [HttpPost]
        public async Task<IActionResult> PostComando([FromBody] Comando comando)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Comandos.Add(comando);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComando", new { id = comando.Id }, comando);
        }

        // DELETE: api/Comandos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComando([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comando = await _context.Comandos.FindAsync(id);
            if (comando == null)
            {
                return NotFound();
            }

            _context.Comandos.Remove(comando);
            await _context.SaveChangesAsync();

            return Ok(comando);
        }

        private bool ComandoExists(Guid id)
        {
            return _context.Comandos.Any(e => e.Id == id);
        }
    }
}