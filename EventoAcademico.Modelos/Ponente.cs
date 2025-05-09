using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoAcademico.Modelos
{
    public class Ponente
    {
        [Key] public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Institucion { get; set; }
        public string Correo { get; set; }
        //FK
        public int CodigoEvento { get; set; }
        public int CodigoSesion { get; set; }

        //Nav
        public List<Evento>? Eventos { get; set; } = new List<Evento>();
    }
}
