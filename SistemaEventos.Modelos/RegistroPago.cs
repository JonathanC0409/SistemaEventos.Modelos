using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventos.Modelos
{
    public class RegistroPago
    {
        [Key] public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string TipoPago { get; set; }
        public double Monto { get; set; }
        //Claves foraneas
        public int InscripcionCodigo { get; set; }
        //Navegadores
        public Inscripcion? Inscripcion { get; set; }

    }
}
