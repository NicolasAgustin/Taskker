using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskker_Desktop.Models.DAL
{
    public class TimeTracked
    {
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public int UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
        public int TareaID { get; set; }
        [ForeignKey("TareaID")]
        public virtual Tarea Tarea { get; set; }
    }
}
