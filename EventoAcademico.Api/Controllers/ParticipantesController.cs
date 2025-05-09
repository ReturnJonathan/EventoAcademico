using EventoAcademico.Api.Data;
using EventoAcademico.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventoAcademico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ParticipantesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            return await _context.Participantes
                .Include(p => p.Inscripciones)
                //.Include(p => p.Pagos)
                //.Include(p => p.Certificados)
                .ToListAsync();
        }
        // GET: api/participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            var participante = await _context.Participantes
                .Include(p => p.Inscripciones)
                //.Include(p => p.Pagos)
                //.Include(p => p.Certificados)
                .FirstOrDefaultAsync(p => p.Codigo == id);
            if (participante == null)
            {
                return NotFound();
            }
            return participante;
        }
        // PUT: api/participantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipante(int id, Participante participante)
        {
            if (id != participante.Codigo)
            {
                return BadRequest();
            }
            _context.Entry(participante).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(id))
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
        // POST: api/participantes
        [HttpPost]
        public async Task<ActionResult<Participante>> PostParticipante(Participante participante)
        {
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetParticipante", new { id = participante.Codigo }, participante);
        }
        // DELETE: api/participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }
            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.Codigo == id);
        }

    }
}
