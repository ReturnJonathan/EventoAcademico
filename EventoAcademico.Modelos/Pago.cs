using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoAcademico.Modelos
{
    public class Pago
    {
        [Key] public int Codigo { get; set; }
        public string MetodoPago { get; set; }
        public double Monto { get; set; }
        public bool Pagado { get; set; }
        //FK
        [ForeignKey(nameof(Inscripcion))]
        public int CodigoInscripcion { get; set; }
        //Nav
        public Inscripcion? Inscripcion { get; set; }


    }
}
