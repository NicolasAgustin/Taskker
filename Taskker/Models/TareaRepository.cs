using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taskker.Models.DAL;
using System.Data.Entity;

namespace Taskker.Models
{
    public class TareaRepository : ITareaRepository, IDisposable
    {
        private TaskkerContext context;
        private bool disposed = false;

        public TareaRepository(TaskkerContext context)
        {
            // Dependency injection
            this.context = context;
        }

        public void DeleteTarea(int tareaId)
        {
            Tarea tarea = context.Tareas.Find(tareaId);
            context.Tareas.Remove(tarea);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            this.disposed = true;
        }

        public Tarea GetTareaByID(int tareaId)
        {
            return context.Tareas.Find(tareaId);
        }

        public IEnumerable<Tarea> GetTareas()
        {
            return context.Tareas.ToList();
        }

        public void InsertTarea(Tarea tarea)
        {
            context.Tareas.Add(tarea);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateTarea(Tarea tarea)
        {
            context.Entry(tarea).State = EntityState.Modified;
        }
    }
}