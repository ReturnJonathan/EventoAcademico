using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoAcademico.Modelos
{
    public class Sesion
    {
        [Key] public int Codigo { get; set; }
        public TimeOnly HorarioInicio { get; set; }
        public TimeOnly HorarioFin { get; set; }
        public string Sala { get; set; }
        //FK
        public int CodigoEvento { get; set; }

        //Nav
        public Evento? Evento { get; set; }
        public List<Inscripcion>? Inscripciones { get; set; } = new List<Inscripcion>();

    }
}
