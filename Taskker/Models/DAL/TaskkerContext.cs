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
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Grupo> grupos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}