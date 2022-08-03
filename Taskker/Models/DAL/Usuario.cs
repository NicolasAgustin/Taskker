using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Taskker.Models.DAL
{
    public class Usuario
    {
        [Key]
        public int usuarioId { get; set; }
        public string nombre_apellido { get; set; }
        public string email { get; set; }
        public string profile_picture_path { get; set; }
        public byte[] e_password { get; set; }

        public virtual ICollection<Grupo> grupos { get; set; }
    }
}