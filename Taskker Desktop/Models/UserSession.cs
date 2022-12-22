using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskker_Desktop.Models.DAL;

namespace Taskker_Desktop.Models
{
    public class UserSession
    {
        public string NombreApellido { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int ID { get; set; }
        public string ProfilePicture { get; set; }

        public UserSession(Usuario user)
        {
            NombreApellido = user.NombreApellido;
            Nombre = user.Nombre;
            Apellido = user.Apellido;
            ID = user.ID;
            ProfilePicture = user.ProfilePicturePath;
        }
    }
}
