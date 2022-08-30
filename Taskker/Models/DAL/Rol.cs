using System.Collections.Generic;

namespace Taskker.Models.DAL
{
    public class Rol
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}