using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taskker.Models.DAL;


namespace Taskker.Models
{
    public class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private TaskkerContext context = new TaskkerContext();

        // agregar mas repositorios aca
        private GenericRepository<Tarea> tareaRepository;
        private GenericRepository<Usuario> usuarioRepository;
        private GenericRepository<Grupo> grupoRepository;
        private GenericRepository<Rol> rolRepository;

        public UsersRoleProvider roleProvider;

        public UnitOfWork()
        {
            this.context = new TaskkerContext();
            this.roleProvider = new UsersRoleProvider();
        }

        public GenericRepository<Tarea> TareaRepository
        {
            get
            {
                if (this.tareaRepository == null)
                {
                    this.tareaRepository = new GenericRepository<Tarea>(context);
                }
                return tareaRepository;
            }
        }

        // agregar mas gets para los repositorios aca

        public GenericRepository<Usuario> UsuarioRepository
        {
            get
            {
                if(this.usuarioRepository == null)
                {
                    this.usuarioRepository= new GenericRepository<Usuario>(context);
                }

                return usuarioRepository;
            }
        }

        public GenericRepository<Grupo> GrupoRepository
        {
            get
            {
                if(this.grupoRepository == null)
                {
                    this.grupoRepository = new GenericRepository<Grupo>(context);
                }

                return grupoRepository;
            }
        }

        public GenericRepository<Rol> RolRepository
        {
            get
            {
                if (this.rolRepository == null)
                {
                    this.rolRepository = new GenericRepository<Rol>(context);
                }
                return rolRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}