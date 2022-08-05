using System;
using Taskker.Models;
using Taskker.Models.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.IO;
namespace Taskker.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        private byte[] hashPassword(string password)
        {
            byte[] result = new SHA512Managed().ComputeHash(
                UTF8Encoding.UTF8.GetBytes(password)
            );

            return result;
        }

        [HttpPost]
        public ActionResult Login(LoginModel _user)
        {
            // Mockear para testear vista con usuarios de la base de datos
            // Falta implementar
            if (!ModelState.IsValid)
                return View(_user);

            // Obtenemos el contexto
            TaskkerContext db = new TaskkerContext();

            byte[] hashed_password = this.hashPassword(_user.Password);

            // Buscamos el usuario en la base de datos
            var user_found = from user in db.Usuarios
                             where _user.Email == user.Email &
                                   hashed_password == user.EncptPassword
                             select user;
            try
            {
                // Single arroja una excepcion
                Usuario user_logged = user_found.Single();
            }catch(InvalidOperationException)
            {
                ModelState.AddModelError("login", "Usuario incorrecto.");
                return View(_user);
            }

            // Hay que redirigir al controlador de grupos o Home
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistroModel _user)
        {
            
            // Chequeamos si el modelo es correcto
            if (!ModelState.IsValid)
                return View(_user);

            // Contexto de la base de datos
            TaskkerContext db = new TaskkerContext();

            // Buscamos si el email ya esta registrado
            var found = from user in db.Usuarios
                        where _user.Email == user.Email
                        select user;

            try
            {
                // Si single no tira excepcion entonces el correo electronico
                // ya esta registrado
                found.Single();
                ModelState.AddModelError("login", "El correo electronico ingresado ya esta en uso.");
                return View(_user);
            }
            catch (InvalidOperationException)
            {
                Usuario nuevo = new Usuario();
                nuevo.Email = _user.Email;
                // Hasheamos la password para guardarla en la base de datos
                nuevo.EncptPassword = this.hashPassword(_user.Password);
                nuevo.NombreApellido = _user.Nombre + " " + _user.Apellido;
                // Si el usuario no subio una foto entonces se asigna la foto por defecto
                if (_user.Photo == null)
                {
                    // Obtenemos el path desde appsettings en web.config
                    nuevo.ProfilePicturePath = ConfigurationManager.AppSettings["DefaultProfile"];
                }
                else
                {
                    string serverPath = ConfigurationManager.AppSettings["ServerDirname"];
                    DirectoryInfo info = new DirectoryInfo(serverPath);
                    
                    // Si el directorio no existe lo creo
                    if (!info.Exists)
                        info.Create();

                    string filename = _user.Photo.FileName;
                    string new_filepath = Path.Combine(serverPath, filename);

                    if (System.IO.File.Exists(new_filepath))
                        System.IO.File.Delete(new_filepath);

                    // Guardamos la foto que subio el usuario
                    _user.Photo.SaveAs(new_filepath);
                    nuevo.ProfilePicturePath = new_filepath;
                }

                // Agregamos el usuario nuevo al contexto
                db.Usuarios.Add(nuevo);
                // Hacemos un commit de los cambios
                db.SaveChanges();
            }

            return View();
        }
    }
}