using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taskker.Models
{
    public class UserSession
    {
        public string NombreApellido { get; set; }
        public string Email { get; set; }
        public string EncodedPicture { get; set; }
        public int ID { get; set; }
    }
}