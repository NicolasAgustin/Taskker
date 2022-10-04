using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskker.Models.DAL;

namespace Taskker.Models
{
    public interface ITareaRepository : IDisposable
    {
        IEnumerable<Tarea> GetTareas();
        Tarea GetTareaByID(int tareaId);
        void InsertTarea(Tarea tarea);
        void DeleteTarea(int tareaId);
        void UpdateTarea(Tarea tarea);
        void Save();
    }
}
