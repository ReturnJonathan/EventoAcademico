using System.ComponentModel.DataAnnotations;

namespace EventoAcademico.Modelos
{
    public class Evento
    {
        [Key] public int Codigo { get; set; }
        public string Nombre { get; set; }
        public DateOnly Fecha { get; set; }
        public string Lugar { get; set; }
        public string Tipo { get; set; }

        //Nav
        public List<Sesion>? Sesiones { get; set; }
        public List<Inscripcion>? Inscripciones { get; set; }
        public List<Ponente>? Ponentes { get; set; }

    }
}
