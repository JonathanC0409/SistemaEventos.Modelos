using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventos.Modelos
{
    public class Participante
    {
        [Key] public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Facultad { get; set; }
        public string Carrera { get; set; }
        public string Nivel { get; set; }
        public string AsistenciaCompleta { get; set; }
        //Claves foraneas

        //Navegadores
        public List<Evento>? Eventos { get; set; }
        public List<Certificado>? Certificados { get; set; }
        public List<Inscripcion>? Inscripciones { get; set; }
    }
}
