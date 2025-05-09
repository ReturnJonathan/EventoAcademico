using EventoAcademico.Api.Data;
using EventoAcademico.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventoAcademico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public InscripcionesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/inscripciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscripcion>>> GetInscripciones()
        {
            return await _context.Inscripciones
                .Include(i => i.Participante)
                .Include(i => i.Evento)
                .Include(i => i.Sesion)
                .Include(i => i.Ponente )
                .ToListAsync();
        }
        // GET: api/inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscripcion>> GetInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones
                .Include(i => i.Participante)
                .Include(i => i.Evento)
                .Include(i => i.Sesion)
                .FirstOrDefaultAsync(i => i.Codigo == id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            return inscripcion;
        }
        // PUT: api/inscripciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcion(int id, Inscripcion inscripcion)
        {
            if (id != inscripcion.Codigo)
            {
                return BadRequest();
            }
            _context.Entry(inscripcion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionExists(id))
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
        // POST: api/inscripciones
        [HttpPost]
        public async Task<ActionResult<Inscripcion>> PostInscripcion(Inscripcion inscripcion)
        {
            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetInscripcion", new { id = inscripcion.Codigo }, inscripcion);
        }
        // DELETE: api/inscripciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool InscripcionExists(int id)
        {
            return _context.Inscripciones.Any(e => e.Codigo == id);
        }
    }
}
