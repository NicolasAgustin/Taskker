﻿using System;
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
        public DbSet<TimeTracked> TiemposTrackeados { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<TaskkerContext>(null);

            // Cada Usuario tiene varios Roles
            modelBuilder.Entity<Rol>()
                .HasMany(r => r.Usuarios)
                .WithMany(u => u.Roles)
                .Map(rol => rol.MapLeftKey("RolID")
                        .MapRightKey("UsuarioID")
                        .ToTable("UsuarioRol"));

            // Cada Grupo tiene muchos Usuarios
            modelBuilder.Entity<Grupo>()
                .HasMany(g => g.Usuarios)
                .WithMany(u => u.Grupos)
                .Map(t => t.MapLeftKey("GrupoID")
                     .MapRightKey("UsuarioID")
                     .ToTable("UsuarioGrupo"));

            // Cada Grupo tiene un Usuario creador
            modelBuilder.Entity<Grupo>()
                .HasRequired(g => g.Usuario)
                .WithMany(ug => ug.CreatedGroups)
                .HasForeignKey(g => g.UsuarioID)
                .WillCascadeOnDelete(false);

            // Cada Tarea tiene muchos Usuarios asignados
            modelBuilder.Entity<Tarea>()
                .HasMany(t => t.Usuarios)
                .WithMany(u => u.Tareas)
                .Map(tb => tb.MapLeftKey("TareaID")
                     .MapRightKey("UsuarioID")
                     .ToTable("UsuarioTarea"));


            // Cada Tarea esta en un Grupo
            modelBuilder.Entity<Tarea>()
                .HasRequired(g => g.Grupo)
                .WithMany(gr => gr.Tareas)
                .HasForeignKey(s => s.GrupoID);

            modelBuilder.Entity<Tarea>()
                .HasMany(tr => tr.TiempoRegistrado)
                .WithRequired(tt => tt.Tarea)
                .HasForeignKey(tr => tr.TareaID);

            modelBuilder.Entity<TimeTracked>()
                .HasRequired(time => time.Usuario)
                .WithMany(u => u.TiempoRegistrado)
                .HasForeignKey(tr => tr.UsuarioID);
        }
    }
}