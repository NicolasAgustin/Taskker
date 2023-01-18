using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskker_Desktop.Models.DAL;

namespace Taskker_Desktop.Models
{
    public class ControlData
    {
        /// <summary>
        /// Class to hold checked data from custom control
        /// </summary>
        public List<Rol> Roles { get; set; }
        public List<Grupo> Grupos { get; set; }
        public string Nombre { get; set; }

    }
}
