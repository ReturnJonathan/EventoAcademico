using EventoAcademico.Api.Data;
using EventoAcademico.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventoAcademico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PagosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PagosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pagos
                .Include(p => p.Inscripcion)
                .ToListAsync();
        
        }
        // GET: api/pagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pagos
                .Include(p => p.Inscripcion)
                .FirstOrDefaultAsync(p => p.Codigo == id);
            if (pago == null)
            {
                return NotFound();
            }
            return pago;
        }
        // PUT: api/pagos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.Codigo)
            {
                return BadRequest();
            }
            _context.Entry(pago).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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
        // POST: api/pagos
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPago", new { id = pago.Codigo }, pago);
        }
        // DELETE: api/pagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.Codigo == id);
        }

    }
}
