﻿using System;
using Taskker.Models.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Taskker.Controllers
{
    public class BrowseController : Controller
    {
        // GET: Browse
        public ActionResult Index()
        {
            TaskkerContext db = new TaskkerContext();

            var tareas = from tarea in db.Tareas
                         select tarea;

            List<Tarea> tareasList = tareas.ToList();

            return View(tareasList);
        }

        [HttpGet]
        public ActionResult CreateTask()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            TaskkerContext db = new TaskkerContext();

            Dictionary<string, string> UserNamePhoto = new Dictionary<string, string>();

            List<string> nombres = new List<string>();

            db.Usuarios.ToList()
                       .ForEach(u => {
                           string encodedPhoto = null;
                           try
                           {
                               byte[] obtainedPicture = System.IO.File.ReadAllBytes(
                                    u.ProfilePicturePath
                               );

                               encodedPhoto = Convert.ToBase64String(
                                    obtainedPicture,
                                    0,
                                    obtainedPicture.Length
                               );

                               encodedPhoto = $"data:image/jpg;base64,{encodedPhoto}";
                           }
                           catch (Exception)
                           {
                               encodedPhoto = string.Empty;
                           }
                           
                           UserNamePhoto.Add(u.NombreApellido, encodedPhoto);
                       });

            return Json(UserNamePhoto, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TaskDetails(string titulo)
        {
            TaskkerContext db = new TaskkerContext();
            
            var tarea = from t in db.Tareas
                        where t.Titulo == titulo
                        select t;

            try
            {
                Tarea display = tarea.Single();
                return PartialView("TaskDetails", display);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
        }
    }
}