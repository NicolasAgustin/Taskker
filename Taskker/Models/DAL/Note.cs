using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taskker.Models.DAL
{
    public class Note
    {
        public string _id { get; set; }
        public string text { get; set; }
        public int created_by { get; set; }
        public bool closed { get; set; }
    }
}