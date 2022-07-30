using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taskker.Models
{
    public class Tarea
    {
        public int TareaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Tipo { get; set; }
        public int TiempoEstimado {get; set;}
        public int TiempoTrackeado { get; set; }
        public virtual Grupo _Grupo { get; set; }
    }
}