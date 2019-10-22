using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque.Db;
using Estoque.Dtos;
using Estoque.Entidades;

namespace Estoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiliaisController : ControllerBase
    {
        private readonly EstoqueContext _context;
        private readonly IMapper _mapper;

        public FiliaisController(EstoqueContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Filiais
        [HttpGet]
        public IEnumerable<Filial> GetFilial()
        {
            return _context.Filiais;
        }

        // GET: api/Filiais/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilial([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filial = await _context.Filiais.FindAsync(id);

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
                if (!FilialExiste(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Filiais
        [HttpPost]
        public async Task<IActionResult> PostFilial([FromBody] FilialInput filialInput )
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            var filialInDb = _context.Filiais.FirstOrDefault(f => f.Nome == filialInput.Nome);

            if (filialInDb != null){
                return BadRequest("Filial já existe com esse nome!");
            }

            var filial = _mapper.Map<Filial>(filialInput);
            await _context.Filiais.AddAsync(filial);
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

            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
            {
                return NotFound();
            }

            _context.Filiais.Remove(filial);
            await _context.SaveChangesAsync();

            return Ok(filial);
        }

        private bool FilialExiste(Guid id)
        {
            return _context.Filiais.Any(e => e.Id == id);
        }
    }
}