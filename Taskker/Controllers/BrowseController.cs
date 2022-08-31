using System;
using System.Linq;
using Taskker.Models;
using System.Web.Mvc;
using Taskker.Models.DAL;
using System.Collections.Generic;

namespace Taskker.Controllers
{

    /*
     * Orden de ejecucion de los filtros
     1. Authentication filter
     2. Authorization filter
     3. Action filter (se ejecuta antes antes del metodo de la accion)
     4. Result filter (se ejecuta antes de generar el resultado de la accion)

     -- Exception filter (se ejecuta cuando se produce una excepcion)

     */

    [Authorize]
    [CustomAuthenticationFilter]
    public class BrowseController : Controller
    {

        TaskkerContext db = new TaskkerContext();

        [HttpGet]
        public ActionResult Index(string grupo)
        {
            // Revisar que valor tiene grupo
            UserSession userSession = Session["UserSession"] as UserSession;

            if (userSession is null)
                return RedirectToAction("Login", "Auth");
            // Buscar todos los grupos a los que pertenece el usuario para mostrarlos en el navbar
            // Si no tiene grupos redireccionar a Groups
            TaskkerContext db = new TaskkerContext();
            Usuario loggedUser = null;
            List<Grupo> grupos = null;
            try 
            {
                var usuario = from user in db.Usuarios
                              where user.Email == userSession.Email
                              select user;

                loggedUser = usuario.Single();

                grupos = loggedUser.Grupos.Concat(loggedUser.CreatedGroups).ToList();

                if (grupos.Count == 0)
                    return RedirectToAction("Index", "Groups");

                List<string> nombresGrupos = new List<string>();
                grupos.ForEach(g => nombresGrupos.Add(g.Nombre));

                Session["Grupos"] = nombresGrupos;
                List<Tarea> tareasList;
                if (grupo == null || !grupos.Exists(g => g.Nombre == grupo))
                {
                    Session["CurrentGroup"] = grupos[0];
                    var tareas = from tarea in grupos[0].Tareas
                                 select tarea;
                    tareasList = tareas.ToList();
                } else
                {
                    var grupoSelected = from _grupo in grupos
                                        where _grupo.Nombre == grupo
                                        select _grupo;

                    var currentGroup = grupoSelected.Single();

                    Session["CurrentGroup"] = currentGroup;

                    tareasList = currentGroup.Tareas.ToList();
                }

                return View(tareasList);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Login", "Auth");
            }
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
                } catch (InvalidOperationException){}
            });

            Tarea newTarea = new Tarea()
            {
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Tipo = (TareaTipo)Enum.Parse(typeof(TareaTipo), t.Tipo),
                Usuarios = asigneesToAdd,
                GrupoID = ((Grupo)Session["CurrentGroup"]).ID
            };

            db.Tareas.Add(newTarea);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateTask(TareaModel tm)
        {

            try
            {
                // Obtenemos la tarea que se debe actualizar
                var tFound = from tarea in db.Tareas
                             where tm.Id == tarea.ID
                             select tarea;

                List<string> usuarios = null;

                if (tm.Asignees != null)
                {
                    usuarios = new List<string>(tm.Asignees.Split(','));
                }

                List<Usuario> filteredUsuarios = new List<Usuario>();
                foreach (var username in usuarios ?? Enumerable.Empty<string>())
                {
                    try
                    {
                        var userfound = db.Usuarios.Where(u => u.NombreApellido == username).Single();
                        filteredUsuarios.Add(userfound);
                    }
                    catch (InvalidOperationException){}
                }

                Tarea tareaFound = tFound.Single();

                bool filter_flag = filteredUsuarios.All(tareaFound.Usuarios.Contains);
                filter_flag = filter_flag && (tareaFound.Descripcion == tm.Descripcion);
                filter_flag = filter_flag && (tareaFound.Titulo == tm.Titulo);
                if (tm.Tipo == null && tareaFound.Tipo == TareaTipo.SinTipo)
                {
                    filter_flag = filter_flag && true;
                }
                else
                {
                    filter_flag = filter_flag && (tareaFound.Tipo == (TareaTipo) Enum.Parse(typeof(TareaTipo), tm.Tipo));
                }
                // Hay que obtener el tipo como numero

                if (!filter_flag)
                {
                    tareaFound.Descripcion = tm.Descripcion;
                    tareaFound.Titulo = tm.Titulo;
                    tareaFound.Usuarios = filteredUsuarios;
                    if (tm.Tipo == null)
                        tareaFound.Tipo = TareaTipo.SinTipo;
                    else
                        tareaFound.Tipo = (TareaTipo) Enum.Parse(typeof(TareaTipo), tm.Tipo);

                    db.SaveChanges();
                }
            } catch (InvalidOperationException){}

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            TaskkerContext db = new TaskkerContext();

            Dictionary<string, string> UserNamePhoto = this.CreateUsersDict(db.Usuarios.ToList());

            return Json(UserNamePhoto, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Funcion para construir un diccionario con el nombre del usuario y su foto de perfil
        /// codificada en base 64
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        private Dictionary<string, string> CreateUsersDict(List<Usuario> usuarios)
        {
            TaskkerContext db = new TaskkerContext();
            Dictionary<string, string> UserNamePhoto = new Dictionary<string, string>();
            usuarios.ForEach(u => {
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
                } catch (Exception)
                {
                    encodedPhoto = string.Empty;
                }

                UserNamePhoto.Add(u.NombreApellido, encodedPhoto);
            });

            return UserNamePhoto;
        }

        [HttpGet]
        [Route("GetUsersInTask/{id:int}")]
        public ActionResult GetUsersInTask(int id)
        {
            TaskkerContext db = new TaskkerContext();

            var tarea = from t in db.Tareas
                        where t.ID == id
                        select t;

            try
            {
                Tarea foundTarea = tarea.Single();
                Dictionary<string, string> UserNamePhoto = this.CreateUsersDict(
                    foundTarea.Usuarios.ToList()
                );
                return Json(UserNamePhoto, JsonRequestBehavior.AllowGet);
            }
            catch (InvalidOperationException)
            {
                return Json(null, JsonRequestBehavior.AllowGet); ;
            }

            
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
                var users = from user in display.Usuarios
                            select user;
                List<Usuario> usuariosAsignados = users.ToList();
                List<(string, Usuario)> photoUsuario = new List<(string, Usuario)>();
                usuariosAsignados.ForEach(user =>
                {
                    photoUsuario.Add(
                        (Utils.EncodePicture(user.ProfilePicturePath), user)
                    );
                });

                ViewData["TupleData"] = photoUsuario;
                return PartialView("TaskDetails", display);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
        }
    }
}