using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventos.Modelos
{
    public class Sesion
    {
        [Key] public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int HorarioInicio { get; set; }
        public int HorarioFin { get; set; }
        public string Sala { get; set; }
        //Claves foraneas
        public int EventoCodigo { get; set; }
        //Navegadores
        public Evento? Evento { get; set; }

    }
}
