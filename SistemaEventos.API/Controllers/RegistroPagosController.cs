using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEventos.Modelos;

namespace SistemaEventos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroPagosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistroPagosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RegistroPagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroPago>>> GetRegistroPagos()
        {
            return await _context.RegistroPagos.ToListAsync();
        }

        // GET: api/RegistroPagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroPago>> GetRegistroPago(int id)
        {
            var registroPago = await _context.RegistroPagos.FindAsync(id);

            if (registroPago == null)
            {
                return NotFound();
            }

            return registroPago;
        }

        // PUT: api/RegistroPagos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistroPago(int id, RegistroPago registroPago)
        {
            if (id != registroPago.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(registroPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroPagoExists(id))
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

        // POST: api/RegistroPagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegistroPago>> PostRegistroPago(RegistroPago registroPago)
        {
            _context.RegistroPagos.Add(registroPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistroPago", new { id = registroPago.Codigo }, registroPago);
        }

        // DELETE: api/RegistroPagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroPago(int id)
        {
            var registroPago = await _context.RegistroPagos.FindAsync(id);
            if (registroPago == null)
            {
                return NotFound();
            }

            _context.RegistroPagos.Remove(registroPago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistroPagoExists(int id)
        {
            return _context.RegistroPagos.Any(e => e.Codigo == id);
        }
    }
}
