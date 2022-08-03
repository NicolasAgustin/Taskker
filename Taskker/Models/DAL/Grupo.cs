using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Taskker.Models.DAL
{
    public class Grupo
    {
        [Key]
        public int grupoId { get; set; }
        public string nombre { get; set; }
        public virtual ICollection<Usuario> usuarios { get; set; }
    }
}