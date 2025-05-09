using EventoAcademico.Api.Data;
using EventoAcademico.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventoAcademico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SesionesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SesionesController(AppDbContext context)
        {
            _context = context;
        }

        //GET : api/sesiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sesion>>> GetSesiones()
        {
            return await _context.Sesiones
                .Include(s => s.Evento)
                .Include(s => s.Inscripciones)
                .ToListAsync();
        }
        //GET : api/sesiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sesion>> GetSesion(int id)
        {
            var sesion = await _context.Sesiones
                .Include(s => s.Evento)
                .Include(s => s.Inscripciones)
                .FirstOrDefaultAsync(s => s.Codigo == id);
            if (sesion == null)
            {
                return NotFound();
            }
            return sesion;
        }
        //PUT : api/sesiones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSesion(int id, Sesion sesion)
        {
            if (id != sesion.Codigo)
            {
                return BadRequest();
            }
            _context.Entry(sesion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SesionExists(id))
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
        //POST : api/sesiones
        [HttpPost]
        public async Task<ActionResult<Sesion>> PostSesion(Sesion sesion)
        {
            _context.Sesiones.Add(sesion);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSesion", new { id = sesion.Codigo }, sesion);
        }
        //DELETE : api/sesiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSesion(int id)
        {
            var sesion = await _context.Sesiones.FindAsync(id);
            if (sesion == null)
            {
                return NotFound();
            }
            _context.Sesiones.Remove(sesion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool SesionExists(int id)
        {
            return _context.Sesiones.Any(e => e.Codigo == id);
        }
    }
}
