using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskker_Desktop.Models.DAL
{
    public static class Context
    {
        public static UnitOfWork unitOfWork = new UnitOfWork();
    }
}
