using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskker_Desktop.Models.DAL;

namespace Taskker_Desktop.Models
{
    public static class UserSession
    {
        public static string NombreApellido { get; set; }
        public static string Nombre { get; set; }
        public static string Apellido { get; set; }
        public static int ID { get; set; }
        public static string ProfilePicture { get; set; }
        public static string EncodedPicture = null;

        public static void setUserData(Usuario user)
        {
            NombreApellido = user.NombreApellido;
            Nombre = user.Nombre;
            Apellido = user.Apellido;
            ID = user.ID;
            ProfilePicture = user.ProfilePicturePath;
        }
    }
}
