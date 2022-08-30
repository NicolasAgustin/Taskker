using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Taskker.Models.DAL
{
    public class Usuario
    {
        [Key]
        public int ID { get; set; }
        public string NombreApellido { get; set; }
        public string Email { get; set; }
        public string ProfilePicturePath { get; set; }
        public byte[] EncptPassword { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<Grupo> CreatedGroups { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
        public virtual ICollection<Rol> Roles { get; set; }
    }

    public class UsuarioData : Usuario
    {
        public string EncodedProfilePicture { get; set; }
    }
}