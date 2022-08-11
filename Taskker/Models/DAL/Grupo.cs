using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Taskker.Models.DAL
{
    public class Grupo
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}