using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Taskker.Models;
using Taskker.Models.DAL;
using System.Configuration;
using System.Web.Security;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel _user)
        {
            //_user.Email = "nicolas.a.sandez@gmail.com";
            //_user.Password = "pass123";
            // Mockear para testear vista con usuarios de la base de datos
            if (!ModelState.IsValid)
                return View(_user);

            byte[] hashed_password = Utils.HashPassword(_user.Password);

            // Buscamos el usuario en la base de datos
            var user_found = from user in unitOfWork.UsuarioRepository.Get(
                                    u => u.Email == _user.Email &&
                                        hashed_password == u.EncptPassword
                                )
                             select user;

            try
            {

                // Single arroja una excepcion
                Usuario user_logged = user_found.Single();

                if (!System.IO.File.Exists(user_logged.ProfilePicturePath))
                {
                    user_logged.ProfilePicturePath = ConfigurationManager.AppSettings["DefaultProfile"];
                }

                FormsAuthentication.SetAuthCookie(user_logged.Email, false);
                FormsAuthentication.SetAuthCookie(
                    Convert.ToString(user_logged.ID),
                    false
                );

                var authTicket = new FormsAuthenticationTicket(
                    1,
                    user_logged.ID.ToString(),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    String.Join(",", user_logged.Roles.ToList())
                );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    encryptedTicket
                );

                HttpContext.Response.Cookies.Add(authCookie);

                UserSession userSession = new UserSession()
                {
                    NombreApellido = Utils.Capitalize(user_logged.NombreApellido),
                    Email = user_logged.Email,
                    EncodedPicture = Utils.EncodePicture(user_logged.ProfilePicturePath),
                    ID = user_logged.ID
                };

                Session["UserSession"] = userSession;
                // Hay que redirigir al controlador de grupos o Home
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
            Session.Clear();
            Roles.DeleteCookie();
            FormsAuthentication.SignOut();
            // Response.Redirect(FormsAuthentication.LoginUrl);
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
                    string new_filepath = Path.Combine(serverPath, filename);

                    if (System.IO.File.Exists(new_filepath))
                        System.IO.File.Delete(new_filepath);

                    // Guardamos la foto que subio el usuario
                    _user.Photo.SaveAs(new_filepath);
                    nuevo.ProfilePicturePath = new_filepath;
                }

                // Agregamos el usuario nuevo al contexto
                unitOfWork.UsuarioRepository.Insert(nuevo);

                AddDefaultRole(nuevo.Email);

                // Hacemos un commit de los cambios
                unitOfWork.Save();

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

        /// <summary>
        /// Funcion para agregar el rol por defecto para el nuevo usuario
        /// </summary>
        /// <param name="email"></param>
        private void AddDefaultRole(string email)
        {
            var user = from u in unitOfWork.UsuarioRepository.Get(u => u.Email == email)
                       select u;

            try
            {
                Usuario userFound = user.Single();

                Rol rol = (
                    from r in unitOfWork.RolRepository.Get(_rol => _rol.Nombre == "Desarrollador")
                    select r
                ).Single();

                userFound.Roles.Add(rol);
                
                unitOfWork.Save();
            }
            catch (InvalidOperationException){}

        }
    }
}