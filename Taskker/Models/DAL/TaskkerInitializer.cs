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
                    nombre_apellido = "nicolas sandez",
                    email = "nicolas.a.sandez@gmail.com",
                    profile_picture_path = "",
                    e_password = new SHA512Managed().ComputeHash(UTF8Encoding.UTF8.GetBytes("pass123")) 
                },
                new Usuario{
                    nombre_apellido = "milagros insaurralde",
                    email = "milagrosinsaurralde382@gmail.com",
                    profile_picture_path = "",
                    e_password = new SHA512Managed().ComputeHash(UTF8Encoding.UTF8.GetBytes("Hoseokytae1"))
                },
                new Usuario{
                    nombre_apellido = "agustin sandez",
                    email = "agustin@gmail.com",
                    profile_picture_path = "",
                    e_password = new SHA512Managed().ComputeHash(UTF8Encoding.UTF8.GetBytes("pass123"))
                }
            };

            usuarios.ForEach(user => context.usuarios.Add(user));
            context.SaveChanges();

            List<Grupo> grupos = new List<Grupo>
            {
                new Grupo{ nombre = "RPA" },
                new Grupo{ nombre = "ODOO" },
                new Grupo{ nombre = "Angular" }
            };

            grupos.ForEach(group => context.grupos.Add(group));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}