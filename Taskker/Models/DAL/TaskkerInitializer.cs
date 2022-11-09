using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Taskker.Models.DAL
{
    public class TaskkerInitializer : System.Data.Entity.DropCreateDatabaseAlways<TaskkerContext>
    {
        protected override void Seed(TaskkerContext context)
        {
            List<Usuario> usuarios = new List<Usuario>
            {
                new Usuario{ 
                    NombreApellido = "nicolas sandez",
                    Email = "nicolas.a.sandez@gmail.com",
                    ProfilePicturePath = "C:\\Users\\Nico\\Desktop\\Server\\nico.jpg",
                    EncptPassword = new SHA512Managed().ComputeHash(UTF8Encoding.UTF8.GetBytes("pass123"))
                },
                new Usuario{
                    NombreApellido = "milagros insaurralde",
                    Email = "milagrosinsaurralde382@gmail.com",
                    ProfilePicturePath = "C:\\Users\\Nico\\Desktop\\Server\\milu.jpg",
                    EncptPassword = new SHA512Managed().ComputeHash(UTF8Encoding.UTF8.GetBytes("Hoseokytae1"))
                },
                new Usuario{
                    NombreApellido = "agustin sandez",
                    Email = "agustin@gmail.com",
                    ProfilePicturePath = "C:\\Users\\Nico\\Desktop\\Server\\default.png",
                    EncptPassword = new SHA512Managed().ComputeHash(UTF8Encoding.UTF8.GetBytes("pass123"))
                }
            };

            usuarios.ForEach(user => context.Usuarios.Add(user));

            context.SaveChanges();

            context.Usuarios.SingleOrDefault(
                u => u.Email == "nicolas.a.sandez@gmail.com"
            ).Roles = new List<Rol>() { new Rol { Nombre = "Project Manager" } };

            context.SaveChanges();

            List<Rol> roles = new List<Rol>
            {
                new Rol{ Nombre = "Project Manager" },
                new Rol{ Nombre = "Analista Funcional" },
                new Rol{ Nombre = "Desarrollador" }
            };

            roles.ForEach(rol => context.Roles.Add(rol));

            context.SaveChanges();

            List<Grupo> grupos = new List<Grupo>
            {
                new Grupo{ 
                    Nombre = "RPA",
                    UsuarioID = usuarios.Single(u => u.Email == "nicolas.a.sandez@gmail.com").ID
                },
                new Grupo{
                    Nombre = "ODOO",
                    UsuarioID = usuarios.Single(u => u.Email == "nicolas.a.sandez@gmail.com").ID
                },
                new Grupo{
                    Nombre = "Angular",
                    UsuarioID = usuarios.Single(u => u.Email == "nicolas.a.sandez@gmail.com").ID
                },
                new Grupo{
                    Nombre = "TEST",
                    UsuarioID = usuarios.Single(u => u.Email == "agustin@gmail.com").ID
                }
            };

            grupos.ForEach(group => context.Grupos.Add(group));

            context.SaveChanges();

            context.Grupos.Single(g => g.ID == 1).Usuarios = usuarios;

            List<Tarea> tareas = new List<Tarea>
            {
                new Tarea{
                    Titulo = "tarea1",
                    Descripcion = "descripcion para tarea 1",
                    Tipo = TareaTipo.SinTipo,
                    Estimado = new DateTime(2022, 1, 1, 1, 0, 0),
                    GrupoID = grupos.Single(g => g.Nombre == "RPA").ID
                }
            };

            tareas.ForEach(task => context.Tareas.Add(task));
            context.SaveChanges();

            List<TimeTracked> tiempos = new List<TimeTracked>()
            {
                new TimeTracked
                {
                    Time = new DateTime(
                        2022, 11, 5, 1, 0, 0
                    ),
                    UsuarioID = 1,
                    TareaID = 1
                }
            };

            tiempos.ForEach(tt => context.TiemposTrackeados.Add(tt));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}