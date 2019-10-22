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
    public class DepositosController : ControllerBase
    {
        private readonly EstoqueContext _context;
        private readonly IMapper _mapper;

        public DepositosController(EstoqueContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Depositos
        [HttpGet]
        public IEnumerable<Deposito> GetDeposito()
        {
            return _context.Depositos;
        }

        // GET: api/Depositos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeposito([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deposito = await _context.Depositos.FindAsync(id);

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
                if (!DepositoExiste(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Depositos
        [HttpPost]
        public async Task<IActionResult> PostDeposito([FromBody] DepositoInput depositoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filialVinculada = _context.Filiais.Find(depositoInput.IdFilialVinculada);

            if (filialVinculada == null){
                return NotFound();
            }

            var deposito = _mapper.Map<Deposito>(depositoInput);
            deposito.FilialId = depositoInput.IdFilialVinculada;
            deposito.FilialVinculada = filialVinculada;
            deposito.DataHora = DateTime.Now;

            _context.Depositos.Add(deposito);
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

            var deposito = await _context.Depositos.FindAsync(id);
            if (deposito == null)
            {
                return NotFound();
            }

            _context.Depositos.Remove(deposito);
            await _context.SaveChangesAsync();

            return Ok(deposito);
        }

        private bool DepositoExiste(Guid id)
        {
            return _context.Depositos.Any(e => e.Id == id);
        }
    }
}