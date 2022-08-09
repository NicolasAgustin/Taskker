using System;
using Taskker.Models;
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
            UserSession userSession = Session["UserSession"] as UserSession;

            if (userSession is null)
                return RedirectToAction("Login", "Auth");
            // Buscar todos los grupos a los que pertenece el usuario para mostrarlos en el navbar
            // Si no tiene grupos redireccionar a Groups
            TaskkerContext db = new TaskkerContext();
            Usuario loggedUser = null;
            try 
            {
                var usuario = from user in db.Usuarios
                              where user.Email == userSession.Email
                              select user;
                loggedUser = usuario.Single();
                if (loggedUser.Grupos.Count == 0)
                {
                    return RedirectToAction("Index", "Groups");
                }

            } catch (InvalidOperationException)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<Grupo> grupos = loggedUser.Grupos.ToList();
            List<string> nombresGrupos = new List<string>();
            grupos.ForEach(g => nombresGrupos.Add(g.Nombre));

            ViewData["Grupos"] = nombresGrupos;

            var tareas = from tarea in grupos[0].Tareas
                         select tarea;

            List<Tarea> tareasList = tareas.ToList();

            return View(tareasList);
        }

        [HttpGet]
        public ActionResult CreateTask()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreateTask(TareaModel t)
        {
            TaskkerContext db = new TaskkerContext();
            List<string> asignees = new List<string>(t.Asignees.Split(','));
            List<Usuario> asigneesToAdd = new List<Usuario>();
            asignees.ForEach(nombre =>
            {
                var found = from user in db.Usuarios
                            where nombre == user.NombreApellido
                            select user;

                try
                {
                    Usuario asigneeFound = found.Single();
                    asigneesToAdd.Add(asigneeFound);
                } catch (InvalidOperationException)
                {

                }

            });

            Tarea newTarea = new Tarea()
            {
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Usuarios = asigneesToAdd,
                GrupoID = 1
            };

            db.Tareas.Add(newTarea);

            db.SaveChanges();

            return RedirectToAction("Index");
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