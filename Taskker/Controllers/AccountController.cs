using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Taskker.Models;
using Taskker.Models.DAL;
using System.Configuration;
using System.Collections.Generic;

namespace Taskker.Controllers
{
    [Authorize]
    [CustomAuthenticationFilter]
    public class AccountController : Controller
    {
        private UnitOfWork unitOfWork;

        public AccountController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            UserSession us = (UserSession)Session["UserSession"];

            Usuario logged = unitOfWork.UsuarioRepository.GetByID(us.ID);

            return View("Profile", logged);
        }

        [HttpPost]
        public ActionResult ModifyProfile(RegistroModel rm)
        {
            UserSession session = (UserSession)Session["UserSession"];
            Usuario toModify = unitOfWork.UsuarioRepository.GetByID(session.ID);

            toModify.Email = rm.Email;
            toModify.NombreApellido = rm.Nombre + " " + rm.Apellido;

            // Solamente cuando se implemente la eliminacion de foto
            // System.IO.File.Delete(toModify.ProfilePicturePath);

            if (rm.Photo == null)
            {
                // Obtenemos el path desde appsettings en web.config
                // toModify.ProfilePicturePath = ConfigurationManager.AppSettings["DefaultProfile"];
                // Si la foto es null entonces no se modifico, habria que tener en cuenta el caso de si se elimina
            }
            else
            {
                string serverPath = ConfigurationManager.AppSettings["ServerDirname"];
                DirectoryInfo info = new DirectoryInfo(serverPath);

                // Si el directorio no existe lo creo
                if (!info.Exists)
                    info.Create();

                
                string extension = Path.GetExtension(rm.Photo.FileName);
                string filename = Path.GetFileName(rm.Photo.FileName);

                string new_filepath = Path.Combine(
                    serverPath, string.Format("{0}_{0}.{0}", filename, Utils.GenerateUUID(), extension)
                );

                if (System.IO.File.Exists(new_filepath))
                    System.IO.File.Delete(new_filepath);

                // Guardamos la foto que subio el usuario
                rm.Photo.SaveAs(new_filepath);
                toModify.ProfilePicturePath = new_filepath;

            }
            
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAccount()
        {
            UserSession session = (UserSession)Session["UserSession"];

            Usuario toDelete = unitOfWork.UsuarioRepository.GetByID(session.ID);

            unitOfWork.UsuarioRepository.Delete(toDelete);

            unitOfWork.Save();

            return RedirectToAction("Logout", "Auth");
        }
    }
}