using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Taskker.Models;
using Taskker.Models.DAL;
using System.Configuration;
using System.Web.Security;
using System.Collections.Generic;
using System.Web;

namespace Taskker.Controllers
{
    public class AuthController : Controller
    {

        private UnitOfWork unitOfWork;
        public AuthController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Crea una cookie de autenticacion en base a un usuario
        /// </summary>
        /// <param name="usuario">Usuario para la cookie</param>
        /// <returns>Cookie de autenticacion</returns>
        private HttpCookie CreateAuthCookie(Usuario usuario)
        {
            FormsAuthentication.SetAuthCookie(usuario.Email, false);
            FormsAuthentication.SetAuthCookie(
                Convert.ToString(usuario.ID),
                false
            );

            /***
             * Creamos el ticket de autenticacion
             * el nombre sera el ID del usuario
             * el tiempo de expiracion esta seteado a 30 minutos
             * Ingresamos tambien los roles del usuario para que puedan ser accedidos por
             * el filtro de autenticacion
             */

            var authTicket = new FormsAuthenticationTicket(
                1,
                usuario.ID.ToString(),
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                String.Join(",", usuario.Roles.ToList())
            );

            // Encriptamos y creamos la cookie
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                encryptedTicket
            );

            return authCookie;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel _user)
        {
            //_user.Email = "nicolas.a.sandez@gmail.com";
            //_user.Password = "pass123";

            if (!ModelState.IsValid)
                return View(_user);

            // Hasheamos la password para ingresarla a la db
            byte[] hashed_password = Utils.HashPassword(_user.Password);

            // Buscamos el usuario en la base de datos
            var user_found = from user in unitOfWork.UsuarioRepository.Get(
                                    u => u.Email == _user.Email &&
                                        hashed_password == u.EncptPassword
                                )
                             select user;

            try
            {

                Usuario user_logged = user_found.Single();

                if (!System.IO.File.Exists(user_logged.ProfilePicturePath))
                {
                    user_logged.ProfilePicturePath = ConfigurationManager.AppSettings["DefaultProfile"];
                }

                // Agregamos la cookie a la respuesta
                HttpContext.Response.Cookies.Add(CreateAuthCookie(user_logged));

                UserSession userSession = new UserSession()
                {
                    NombreApellido = Utils.Capitalize(user_logged.NombreApellido),
                    Email = user_logged.Email,
                    EncodedPicture = Utils.EncodePicture(user_logged.ProfilePicturePath),
                    ID = user_logged.ID
                };

                Session["UserSession"] = userSession;

                return RedirectToAction("Index", "Browse");
            }
            catch(InvalidOperationException)
            {
                ModelState.AddModelError("login", "Usuario incorrecto.");
                return View(_user);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            // Limpiamos los datos de sesion
            Session.Clear();

            // Eliminamos la cookie
            Roles.DeleteCookie();
            
            // Eliminamos el ticket de autenticacion
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistroModel _user)
        {
            
            // Chequeamos si el modelo es correcto
            if (!ModelState.IsValid)
                return View(_user);

            // Buscamos si el email ya esta registrado
            var found = from user in unitOfWork.UsuarioRepository.Get(u => u.Email == _user.Email)
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
                nuevo.EncptPassword = Utils.HashPassword(_user.Password);

                // El rol por defecto siempre sera desarrollador
                Rol defaultRole = unitOfWork.RolRepository.Get(r => r.Nombre == "Desarrollador").FirstOrDefault();

                nuevo.Roles = new List<Rol>() { defaultRole };
                nuevo.Nombre = _user.Nombre;
                nuevo.Apellido = _user.Apellido;
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

                    string extension = Path.GetExtension(filename);
                    string filenameOnly = Path.GetFileName(filename);
                    filenameOnly = filenameOnly.Replace(extension, "");
                    // Combinamos el path agregandole un UUID unico

                    string uuid = Utils.GenerateUUID();
                    string new_filepath = Path.Combine(
                        serverPath, string.Format("{0}_{1}.{2}", filenameOnly, uuid, extension)
                    );

                    if (System.IO.File.Exists(new_filepath))
                        System.IO.File.Delete(new_filepath);

                    // Guardamos la foto que subio el usuario
                    _user.Photo.SaveAs(new_filepath);
                    nuevo.ProfilePicturePath = new_filepath;
                }

                // Agregamos el usuario nuevo al contexto
                unitOfWork.UsuarioRepository.Insert(nuevo);

                //AddDefaultRole(nuevo.Email);

                // Hacemos un commit de los cambios
                unitOfWork.Save();

                HttpContext.Response.Cookies.Add(CreateAuthCookie(nuevo));

                UserSession userSession = new UserSession()
                {
                    NombreApellido = nuevo.NombreApellido,
                    Email = nuevo.Email,
                    EncodedPicture = Utils.EncodePicture(nuevo.ProfilePicturePath),
                    ID = nuevo.ID
                };

                Session["UserSession"] = userSession;
            }

            return RedirectToAction("Index", "Browse");
        }
    }
}