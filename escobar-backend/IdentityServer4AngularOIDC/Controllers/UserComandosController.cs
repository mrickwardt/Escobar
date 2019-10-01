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
    public class UserComandosController : ControllerBase
    {
        private readonly UserContext _context;

        public UserComandosController(UserContext context)
        {
            _context = context;
        }

        // GET: api/UserComandos
        [HttpGet]
        public IEnumerable<UserComandos> GetUserComandos()
        {
            return _context.UserComandos;
        }

        // GET: api/UserComandos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserComandos([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userComandos = await _context.UserComandos.FindAsync(id);

            if (userComandos == null)
            {
                return NotFound();
            }

            return Ok(userComandos);
        }
      

        // PUT: api/UserComandos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserComandos([FromRoute] Guid id, [FromBody] UserComandos userComandos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userComandos.Id)
            {
                return BadRequest();
            }

            var user = _context.Users.FirstOrDefault(x => x.ID == userComandos.UserId);
            if (user == null)
            {
                return Forbid();
            }

            var comando = _context.Comandos.FirstOrDefault(x => x.Id == userComandos.ComandoId);
            if (comando == null)
            {
                return Forbid();
            }

            _context.Entry(userComandos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserComandosExists(id))
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

        // POST: api/UserComandos
        [HttpPost]
        public async Task<IActionResult> PostUserComandos([FromBody] UserComandos userComandos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.FirstOrDefault(x => x.ID == userComandos.UserId);
            if (user == null)
            {
                return Forbid();
            }

            var comando = _context.Comandos.FirstOrDefault(x => x.Id == userComandos.ComandoId);
            if (comando == null)
            {
                return Forbid();
            }

            _context.UserComandos.Add(userComandos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserComandos", new { id = userComandos.Id }, userComandos);
        }

        // DELETE: api/UserComandos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserComandos([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userComandos = await _context.UserComandos.FindAsync(id);
            if (userComandos == null)
            {
                return NotFound();
            }

            _context.UserComandos.Remove(userComandos);
            await _context.SaveChangesAsync();

            return Ok(userComandos);
        }

        private bool UserComandosExists(Guid id)
        {
            return _context.UserComandos.Any(e => e.Id == id);
        }
    }
}