using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskker.Models.DAL
{
    public class Tarea
    {
        [Key]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public TareaTipo Tipo { get; set; }
        public int GrupoID { get; set; }
        [ForeignKey("GrupoID")]
        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}