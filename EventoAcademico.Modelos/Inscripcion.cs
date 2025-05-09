using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoAcademico.Modelos
{
    public class Inscripcion
    {
        [Key] public int Codigo { get; set; }
        public Boolean Estado { get; set; }
        public DateOnly FechaInscripcion { get; set; }

        //FK
        public int CodigoEvento { get; set; }
        public int CodigoParticipante { get; set; }
        public int CodigoSesion { get; set; }
        public int CodigoPonente { get; set; }


        //Nav
        public Participante? Participante { get; set; }
        public Evento? Evento { get; set; }
        public Sesion? Sesion { get; set; }

        public Ponente? Ponente { get; set; }
        public Certificado? Certificado { get; set; }
        public Pago? Pago { get; set; }



    }
}
