using EventoAcademico.Api.Data;
using EventoAcademico.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventoAcademico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EventosController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
            return await _context.Eventos
                .Include(e => e.Sesiones)
                .Include(e => e.Inscripciones)
                .Include(e => e.Ponentes)
                .ToListAsync();
        }
        // GET: api/eventos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
            var evento = await _context.Eventos
                .Include(e => e.Sesiones)
                .Include(e => e.Inscripciones)
                .Include(e => e.Ponentes)
                .FirstOrDefaultAsync(e => e.Codigo == id);
            if (evento == null)
            {
                return NotFound();
            }
            return evento;
        }
        // PUT: api/eventos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, Evento evento)
        {
            if (id != evento.Codigo)
            {
                return BadRequest();
            }
            _context.Entry(evento).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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
        // POST: api/eventos
        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEvento", new { id = evento.Codigo }, evento);
        }
        // DELETE: api/eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Codigo == id);
        }
    }
}
