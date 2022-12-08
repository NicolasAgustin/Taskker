﻿using System;
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
            // Session del usuario
            UserSession us = (UserSession)Session["UserSession"];

            // Obtenemos el usuario logeado
            Usuario logged = unitOfWork.UsuarioRepository.GetByID(us.ID);

            return View("Profile", logged);
        }

        /// <summary>
        /// Modifica un usuario en base a un modelo pasado por la vista
        /// </summary>
        /// <param name="rm">Modelo de la vista</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModifyProfile(RegistroModel rm)
        {
            UserSession session = (UserSession)Session["UserSession"];
            
            // Obtenemos el usuario a modificar
            Usuario toModify = unitOfWork.UsuarioRepository.GetByID(session.ID);

            // Asumimos que siempre se modifica

            toModify.Email = rm.Email;
            toModify.NombreApellido = rm.Nombre + " " + rm.Apellido;

            UserSession us = (UserSession)Session["UserSession"];
            us.Email = rm.Email;
            us.NombreApellido = toModify.NombreApellido;

            // Solamente cuando se implemente la eliminacion de foto
            // System.IO.File.Delete(toModify.ProfilePicturePath);

            if (rm.Photo == null)
            {
                // Obtenemos el path desde appsettings en web.config
                toModify.ProfilePicturePath = ConfigurationManager.AppSettings["DefaultProfile"];
                // Si la foto es null entonces no se modifico, habria que tener en cuenta el caso de si se elimina
            }
            else
            {
                // Obtenemos el directorio desde las configuraciones
                string serverPath = ConfigurationManager.AppSettings["ServerDirname"];
                DirectoryInfo info = new DirectoryInfo(serverPath);

                // Si el directorio no existe lo creo
                if (!info.Exists)
                    info.Create();

                
                string extension = Path.GetExtension(rm.Photo.FileName);
                string filename = Path.GetFileName(rm.Photo.FileName);

                // Combinamos el path agregandole un UUID unico
                /***
                 * TODO:
                 * revisar por que no pone el uuid
                 
                 */
                string new_filepath = Path.Combine(
                    serverPath, string.Format("{0}_{0}.{0}", filename, Utils.GenerateUUID(), extension)
                );

                if (System.IO.File.Exists(new_filepath))
                    System.IO.File.Delete(new_filepath);

                // Guardamos la foto que subio el usuario
                rm.Photo.SaveAs(new_filepath);
                toModify.ProfilePicturePath = new_filepath;
                us.EncodedPicture = Utils.EncodePicture(new_filepath);
            }

            unitOfWork.Save();

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