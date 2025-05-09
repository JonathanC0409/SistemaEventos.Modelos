using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventos.Modelos
{
    public class Evento
    {
        [Key] public int Codigo { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Lugar { get; set; }
        public string Tipo { get; set; }
        //Claves foraneas
        public int PonenteCodigo { get; set; }
        public int ParticipanteCodigo { get; set; }
        //Navegadores
        public Ponente? Ponente { get; set; }
        public Participante? Participante { get; set; }
        public List<Sesion>? Sesiones { get; set; } 

    }
}
