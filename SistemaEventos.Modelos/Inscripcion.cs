using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventos.Modelos
{
    public class Inscripcion
    {
        [Key] public int Codigo { get; set; }
        public bool Pago { get; set; }
        public string Estado { get; set; }
        public DateTime FechaInscripcion { get; set; }
        //Claves foraneas
        public int ParticipanteCodigo { get; set; }
        //Navegadores
        public Participante? Participante { get; set; }
        public List<RegistroPago>? RegistroPagos { get; set; }

    }
}
