using EventoAcademico.Api.Data;
using EventoAcademico.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventoAcademico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PonentesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PonentesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/ponentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ponente>>> GetPonentes()
        {
            return await _context.Ponentes
                .Include(p => p.Eventos)
                .ToListAsync();
        }
        // GET: api/ponentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ponente>> GetPonente(int id)
        {
            var ponente = await _context.Ponentes
                .Include(p => p.Eventos)
                .FirstOrDefaultAsync(p => p.Codigo == id);
            if (ponente == null)
            {
                return NotFound();
            }
            return ponente;
        }
        // PUT: api/ponentes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPonente(int id, Ponente ponente)
        {
            if (id != ponente.Codigo)
            {
                return BadRequest();
            }
            _context.Entry(ponente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PonenteExists(id))
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
        // POST: api/ponentes
        [HttpPost]
        public async Task<ActionResult<Ponente>> PostPonente(Ponente ponente)
        {
            _context.Ponentes.Add(ponente);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPonente", new { id = ponente.Codigo }, ponente);
        }
        // DELETE: api/ponentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePonente(int id)
        {
            var ponente = await _context.Ponentes.FindAsync(id);
            if (ponente == null)
            {
                return NotFound();
            }
            _context.Ponentes.Remove(ponente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool PonenteExists(int id)
        {
            return _context.Ponentes.Any(e => e.Codigo == id);
        }
    }
}
