using EventoAcademico.Api.Data;
using EventoAcademico.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventoAcademico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CertificadosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/certificados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificado>>> GetCertificados()
        {
            return await _context.Certificados
                .Include(c => c.Inscripcion)
                    .ThenInclude(i => i.Pago)
                .ToListAsync();
        }

        // GET: api/certificados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Certificado>> GetCertificado(int id)
        {
            var cert = await _context.Certificados
                .Include(c => c.Inscripcion)
                    .ThenInclude(i => i.Pago)
                .FirstOrDefaultAsync(c => c.Codigo == id);

            if (cert == null)
                return NotFound();
            return cert;
        }

        // PUT genérico: api/certificados/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificado(int id, Certificado certificado)
        {
            if (id != certificado.Codigo)
                return BadRequest();

            _context.Entry(certificado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        

        // DELETE genérico: api/certificados/5
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificado(int id)
        {
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado == null)
                return NotFound();

            _context.Certificados.Remove(certificado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        

        // ---------------------------------------
        // POST controlado: sólo emitir si Pago.Pagado y AsistenciaCompleta == true
        // POST: api/certificados
        [HttpPost]
        public async Task<IActionResult> PostCertificado(Certificado certificado)
        {
            // 1) Cargar la inscripción con su pago
            var ins = await _context.Inscripciones
                .Include(i => i.Pago)
                .FirstOrDefaultAsync(i => i.Codigo == certificado.CodigoInscripcion);

            if (ins == null)
                return NotFound(new { mensaje = "Inscripción no encontrada." });

            // 2) Verificar pago
            if (ins.Pago == null || !ins.Pago.Pagado)
                return BadRequest(new { mensaje = "El pago no ha sido procesado." });

            // 3) Verificar asistencia
            if (!certificado.Asistencia)
                return BadRequest(new { mensaje = "La asistencia no ha sido confirmada." });

            // 4) Verificar que aún no exista
            var existente = await _context.Certificados
                .FirstOrDefaultAsync(c => c.CodigoInscripcion == ins.Codigo);
            if (existente != null)
                return Conflict(new { mensaje = "Ya existe un certificado para esta inscripción." });

            // 5) Asignar fecha y guardar
            certificado.FechaEmision = DateTime.UtcNow;
            _context.Certificados.Add(certificado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCertificado),
                new { id = certificado.Codigo },
                certificado
            );
        }

        private bool CertificadoExists(int id) =>
            _context.Certificados.Any(e => e.Codigo == id);
    }
}
