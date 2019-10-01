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
    public class FiliaisController : ControllerBase
    {
        private readonly EstoqueContext _context;

        public FiliaisController(EstoqueContext context)
        {
            _context = context;
        }

        // GET: api/Filiais
        [HttpGet]
        public IEnumerable<Filial> GetFilial()
        {
            return _context.Filial;
        }

        // GET: api/Filiais/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilial([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filial = await _context.Filial.FindAsync(id);

            if (filial == null)
            {
                return NotFound();
            }

            return Ok(filial);
        }

        // PUT: api/Filiais/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilial([FromRoute] Guid id, [FromBody] Filial filial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filial.Id)
            {
                return BadRequest();
            }

            _context.Entry(filial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilialExists(id))
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

        // POST: api/Filiais
        [HttpPost]
        public async Task<IActionResult> PostFilial([FromBody] Filial filial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Filial.Add(filial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilial", new { id = filial.Id }, filial);
        }

        // DELETE: api/Filiais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilial([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filial = await _context.Filial.FindAsync(id);
            if (filial == null)
            {
                return NotFound();
            }

            _context.Filial.Remove(filial);
            await _context.SaveChangesAsync();

            return Ok(filial);
        }

        private bool FilialExists(Guid id)
        {
            return _context.Filial.Any(e => e.Id == id);
        }
    }
}