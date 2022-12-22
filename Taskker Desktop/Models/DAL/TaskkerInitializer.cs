using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Taskker_Desktop.Models.DAL
{
    public class TaskkerInitializer : System.Data.Entity.CreateDatabaseIfNotExists<TaskkerContext>
    {
        protected override void Seed(TaskkerContext context)
        {
            
        }
    }
}
