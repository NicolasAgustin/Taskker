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
            Usuario user_logged = unitOfWork.UsuarioRepository.Get(
                u => u.Email == _user.Email && hashed_password == u.EncptPassword).SingleOrDefault();

            if (user_logged == null)
            {
                ModelState.AddModelError("login", "Usuario incorrecto.");
                return View(_user);
            }

            string pictureEncoded = null;

            if (user_logged.EncodedProfilePicture == null)
            {
                pictureEncoded = Utils.EncodePicture(ConfigurationManager.AppSettings["DefaultProfile"]);
            } else
            {
                pictureEncoded = Utils.EncodePictureFromBase64(user_logged.EncodedProfilePicture);
            }

            // Agregamos la cookie a la respuesta
            HttpContext.Response.Cookies.Add(CreateAuthCookie(user_logged));

            UserSession userSession = new UserSession()
            {
                NombreApellido = Utils.Capitalize(user_logged.NombreApellido),
                Email = user_logged.Email,
                EncodedPicture = pictureEncoded,
                ID = user_logged.ID
            };

            Session["UserSession"] = userSession;

            return RedirectToAction("Index", "Browse");

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
            Usuario found = unitOfWork.UsuarioRepository.Get(u => u.Email == _user.Email).SingleOrDefault();

            if (found != null)
            {
                ModelState.AddModelError("login", "El correo electronico ingresado ya esta en uso.");
                return View(_user);
            }


            Usuario nuevo = new Usuario();
            nuevo.Email = _user.Email;
            // Hasheamos la password para guardarla en la base de datos
            nuevo.EncptPassword = Utils.HashPassword(_user.Password);

            // El rol por defecto siempre sera desarrollador
            Rol defaultRole = unitOfWork.RolRepository.Get(r => r.Nombre == "Desarrollador").SingleOrDefault();

            nuevo.Roles = new List<Rol>() { defaultRole };
            nuevo.Nombre = _user.Nombre;
            nuevo.Apellido = _user.Apellido;
            // Si el usuario no subio una foto entonces se asigna la foto por defecto
            if (_user.Photo == null)
            {
                // Obtenemos el path desde appsettings en web.config
                nuevo.EncodedProfilePicture = Utils.EncodePicture(ConfigurationManager.AppSettings["DefaultProfile"]);
            }
            else
            {
                nuevo.EncodedProfilePicture = Utils.EncodeFromStream(_user.Photo.InputStream);
            }

            // Agregamos el usuario nuevo al contexto
            unitOfWork.UsuarioRepository.Insert(nuevo);

            // Hacemos un commit de los cambios
            unitOfWork.Save();

            HttpContext.Response.Cookies.Add(CreateAuthCookie(nuevo));

            UserSession userSession = new UserSession()
            {
                NombreApellido = nuevo.NombreApellido,
                Email = nuevo.Email,
                EncodedPicture = nuevo.EncodedProfilePicture,
                ID = nuevo.ID
            };

            Session["UserSession"] = userSession;


            return RedirectToAction("Index", "Browse");
        }
    }
}