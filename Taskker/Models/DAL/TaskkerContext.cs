using System;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Taskker.Models.DAL
{
    public class TaskkerContext : DbContext
    {
        // Le pasamos la clave que tiene nuestra connection string en web.config
        public TaskkerContext() : base("TaskkerContext")
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Grupo>()
                .HasMany(g => g.Usuarios)
                .WithMany(u => u.Grupos)
                .Map(t => t.MapLeftKey("GrupoID")
                     .MapRightKey("UsuarioID")
                     .ToTable("UsuarioGrupo"));

            modelBuilder.Entity<Grupo>()
                .HasRequired(g => g.Usuario)
                .WithMany(ug => ug.CreatedGroups)
                .HasForeignKey(g => g.UsuarioID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tarea>()
                .HasMany(t => t.Usuarios)
                .WithMany(u => u.Tareas)
                .Map(tb => tb.MapLeftKey("TareaID")
                     .MapRightKey("UsuarioID")
                     .ToTable("UsuarioTarea"));

            modelBuilder.Entity<Tarea>()
                .HasRequired(g => g.Grupo)
                .WithMany(gr => gr.Tareas)
                .HasForeignKey(s => s.GrupoID);

        }
    }
}