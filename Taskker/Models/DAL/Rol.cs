using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskker.Models.DAL
{
    public class Rol
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}