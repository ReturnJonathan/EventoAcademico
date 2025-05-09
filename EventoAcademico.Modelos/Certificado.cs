using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoAcademico.Modelos
{
    public class Certificado
    {
        [Key] public int Codigo { get; set; } 
        public DateTime FechaEmision { get; set; }
        public Boolean Asistencia { get; set; }
        public string TipoCertificado { get; set; }
        public string NombreCertificado { get; set; }
        public string Descripcion { get; set; }
        //FK
        [ForeignKey(nameof(Inscripcion))]
        public int CodigoInscripcion { get; set; }

        //Nav
        public Inscripcion? Inscripcion { get; set; }

    }
}
