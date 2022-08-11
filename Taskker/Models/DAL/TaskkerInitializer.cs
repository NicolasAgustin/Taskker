using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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
                }
            };

            grupos.ForEach(group => context.Grupos.Add(group));

            context.SaveChanges();
            //Usuario owner = context.Usuarios.Single(u => u.Email == "nicolas.a.sandez@gmail.com");
            //context.Grupos.Single(g => g.Nombre == "RPA").UsuarioID = owner.ID;
            //owner.CreatedGroups.Add(grupos.Single(g => g.Nombre == "RPA"));


            List<Tarea> tareas = new List<Tarea>
            {
                new Tarea{
                    Titulo = "tarea1",
                    Descripcion = "descripcion para tarea 1",
                    GrupoID = grupos.Single(g => g.Nombre == "RPA").ID
                }
            };

            tareas.ForEach(task => context.Tareas.Add(task));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}