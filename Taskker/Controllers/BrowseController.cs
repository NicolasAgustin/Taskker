using System;
using System.Linq;
using Taskker.Models;
using System.Web.Mvc;
using Taskker.Models.DAL;
using System.Collections.Generic;
using System.Web.Security;

namespace Taskker.Controllers
{

    /*
     * Orden de ejecucion de los filtros
     1. Authentication filter
     2. Authorization filter
     3. Action filter (se ejecuta antes antes del metodo de la accion)
     4. Result filter (se ejecuta antes de generar el resultado de la accion)

     -- Exception filter (se ejecuta cuando se produce una excepcion)


        Cuando usamos context.Tareas estamos usando un tipo IQueryable que no es enviado
        a la db hasta que se convierte en una coleccion usando un metodo como ToList,
        lo que no ocurre hasta que utilizamos el modelo.
        
        La diferencia de usar context.Tareas y tareaRepository.GetTareas()
        reside en que si luego realizamos un filtrado con Where()
        en la primer opcion el where se convierte en un WHERE directamente en la query SQL
        mientras que con la segunda opcion obtenemos los valores en memoria y cada filtrado que
        realicemos se hara en memoria.

     */

    [Authorize]
    [CustomAuthenticationFilter]
    public class BrowseController : Controller
    {
        private UnitOfWork unitOfWork;

        public BrowseController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult Index(string grupo)
        {
            // Revisar que valor tiene grupo
            UserSession userSession = Session["UserSession"] as UserSession;

            if (userSession is null)
                return RedirectToAction("Login", "Auth");
            // Buscar todos los grupos a los que pertenece el usuario para mostrarlos en el navbar
            // Si no tiene grupos redireccionar a Groups
            Usuario loggedUser = null;
            List<Grupo> grupos = null;
            try 
            {
                var usuario = from user in unitOfWork
                                           .UsuarioRepository
                                           .Get(u => u.Email == userSession.Email)
                              select user;

                loggedUser = usuario.Single();

                List<string> roles = new List<string>();

                loggedUser.Roles.ToList().ForEach(
                        rol => roles.Add(rol.Nombre)
                );

                ViewData["Roles"] = roles;

                grupos = loggedUser.Grupos.Concat(loggedUser.CreatedGroups).Distinct().ToList();

                if (grupos.Count == 0)
                    return RedirectToAction("Index", "Groups");

                List<string> nombresGrupos = new List<string>();
                grupos.ForEach(g => nombresGrupos.Add(g.Nombre));

                Session["Grupos"] = nombresGrupos;
                List<Tarea> tareasList;

                Grupo currentGroup;

                if (grupo == null || !grupos.Exists(g => g.Nombre == grupo))
                {
                    Session["CurrentGroup"] = grupos[0];
                    currentGroup = grupos[0];
                    var tareas = from tarea in grupos[0].Tareas
                                 select tarea;
                    tareasList = tareas.ToList();
                } else
                {
                    var grupoSelected = from _grupo in grupos
                                        where _grupo.Nombre == grupo
                                        select _grupo;

                    currentGroup = grupoSelected.Single();

                    Session["CurrentGroup"] = currentGroup;
                }

                return View(currentGroup);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        [Route("DeleteTimeTracked/{id:int}")]
        public ActionResult DeleteTimeTracked(int id)
        {
            unitOfWork.TtrackedRepository.Delete(id);
            unitOfWork.Save();
            return null;
        }

        [HttpGet]
        [AuthorizeRoleAttribute("Project Manager")]
        public ActionResult CreateTask()
        {
            return PartialView();
        }

        [HttpPost]
        [AuthorizeRoleAttribute("Project Manager")]
        public ActionResult CreateTask(TareaModel t)
        {
            DateTime timeEstimated = Utils.parseTime(t.Estimado);
            List<string> asignees = new List<string>(t.Asignees.Split(','));
            List<Usuario> asigneesToAdd = new List<Usuario>();
            asignees.ForEach(nombre =>
            {
                var found = from user in unitOfWork
                                         .UsuarioRepository
                                         .Get(u => nombre == u.NombreApellido)
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
                Estimado = timeEstimated,
                Usuarios = asigneesToAdd,
                GrupoID = ((Grupo)Session["CurrentGroup"]).ID
            };

            unitOfWork.TareaRepository.Insert(newTarea);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateTask(TareaModel tm)
        {

            try
            {
                // Obtenemos la tarea que se debe actualizar
                var tareaFound = unitOfWork.TareaRepository.GetByID(tm.Id);

                List<string> usuarios = null;

                UserSession us = (UserSession) Session["UserSession"];

                var user = from u in unitOfWork
                                     .UsuarioRepository
                                     .Get(u => u.Email == us.Email)
                           select u;

                Usuario found_user = user.Single();

                if (tm.Asignees != null)
                {
                    usuarios = new List<string>(tm.Asignees.Split(','));
                }

                List<Usuario> filteredUsuarios = new List<Usuario>();
                foreach (var username in usuarios ?? Enumerable.Empty<string>())
                {
                    try
                    {
                        var userfound = from u in unitOfWork.UsuarioRepository
                                            .Get(_us => _us.NombreApellido == username)
                                        select u;
                        
                        filteredUsuarios.Add(userfound.Single());
                    }
                    catch (InvalidOperationException){}
                }

                TimeTracked tiempo = null;
                bool newTrackedTime = false;
                try
                {
                    tiempo = tareaFound.TiempoRegistrado.Single(
                        tr => tr.TareaID == tareaFound.ID &&
                              tr.UsuarioID == found_user.ID
                    );
                } catch (InvalidOperationException)
                {
                    if (tm.TiempoRegistrado != null)
                    {
                        tiempo = new TimeTracked
                        {
                            UsuarioID = found_user.ID,
                            TareaID = tareaFound.ID,
                            Time = Utils.parseTime(tm.TiempoRegistrado)
                        };

                        unitOfWork.TtrackedRepository.Insert(tiempo);

                        newTrackedTime = true;
                    }
                }

                tareaFound.Descripcion = tm.Descripcion;
                tareaFound.Titulo = tm.Titulo;
                tareaFound.Usuarios = filteredUsuarios;
                tareaFound.Estimado = Utils.parseTime(tm.Estimado);

                if (tm.TiempoRegistrado != null && !newTrackedTime)
                {
                    DateTime parsedTime = Utils.parseTime(tm.TiempoRegistrado);

                    tiempo.Time = tiempo.Time
                        .AddHours(parsedTime.Hour)
                        .AddMinutes(parsedTime.Minute)
                        .AddSeconds(parsedTime.Second);
                }

                if (tm.Tipo == null)
                    tareaFound.Tipo = TareaTipo.SinTipo;
                else
                    tareaFound.Tipo = (TareaTipo)Enum.Parse(
                        typeof(TareaTipo),
                        tm.Tipo
                    );
                unitOfWork.TareaRepository.Update(tareaFound);
                unitOfWork.Save();

            }
            catch (InvalidOperationException){}

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
                
                usuariosAsignados.ForEach(user => {
                        photoUsuario.Add(
                            (Utils.EncodePicture(user.ProfilePicturePath), user)
                        );
                    }
                );

                ViewData["TupleData"] = photoUsuario;

                List<(DateTime, int, int, int)> displayTimes = 
                    new List<(DateTime, int, int, int)>();

                var tiemposTarea = unitOfWork.TtrackedRepository.Get(tt => tt.TareaID == display.ID);

                tiemposTarea.ToList().ForEach(t =>
                {
                    displayTimes.Add((t.Time, t.TareaID, t.UsuarioID, t.ID));
                });

                ViewData["Times"] = displayTimes;
                ViewBag.Time = Utils.generateStringEstimatedTime(display.Estimado);

                return PartialView("TaskDetails", display);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
        }
    }
}