using EventoAcademico.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EventoAcademico.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Evento> Eventos { get; set; } = default;
        public DbSet<Sesion> Sesiones { get; set; } = default;
        public DbSet<Ponente> Ponentes { get; set; } = default;
        public DbSet<Participante> Participantes { get; set; } = default;
        public DbSet<Inscripcion> Inscripciones { get; set; } = default;
        public DbSet<Pago> Pagos { get; set; } = default;
        public DbSet<Certificado> Certificados { get; set; } = default;



    }
}
