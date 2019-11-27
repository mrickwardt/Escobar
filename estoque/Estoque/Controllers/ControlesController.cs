using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estoque.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque.Db;
using Estoque.Entidades;
using AutoMapper;

namespace Estoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlesController : ControllerBase
    {
        private readonly EstoqueContext _context;
        private readonly IMapper _mapper;

        public ControlesController(EstoqueContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Controles
        [HttpGet]
        public IEnumerable<Controle> GetControles()
        {
            return _context.Controles;
        }

        // GET: api/Controles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetControle([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var controle = await _context.Controles.FindAsync(id);

            if (controle == null)
            {
                return NotFound();
            }

            return Ok(controle);
        }

        // PUT: api/Controles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControle([FromRoute] Guid id, [FromBody] Controle controle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != controle.Id)
            {
                return BadRequest();
            }

            _context.Entry(controle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControleExists(id))
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

        // POST: api/Controles
        [HttpPost("Criar")]
        public async Task<IActionResult> PostControle([FromBody] Controle controle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Controles.Add(controle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControle", new { id = controle.Id }, controle);
        }

        // DELETE: api/Controles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControle([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var controle = await _context.Controles.FindAsync(id);
            if (controle == null)
            {
                return NotFound();
            }

            _context.Controles.Remove(controle);
            await _context.SaveChangesAsync();

            return Ok(controle);
        }

        private bool ControleExists(Guid id)
        {
            return _context.Controles.Any(e => e.Id == id);
        }
    }
}