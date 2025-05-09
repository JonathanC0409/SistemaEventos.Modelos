using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventos.Modelos
{
    public class Certificado
    {
        [Key] public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string TipoCertificado { get; set; }
        //Claves foraneas
        public int ParticipanteCodigo { get; set; }
        //Navegadores
        public Participante? Participante { get; set; }

    }
}
