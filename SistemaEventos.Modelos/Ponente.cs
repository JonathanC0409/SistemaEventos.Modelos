using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventos.Modelos
{
    public class Ponente
    {
        [Key] public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Pais { get; set; }
        //Claves foraneas

        //Navegadores
        public List<Evento>? Eventos { get; set; }
    }
}
