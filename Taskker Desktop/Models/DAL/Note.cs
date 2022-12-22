using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskker_Desktop.Models.DAL
{
    class Note
    {
        public string _id { get; set; }
        public string text { get; set; }
        public int created_by { get; set; }
        public bool closed { get; set; }
    }
}
