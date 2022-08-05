using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}