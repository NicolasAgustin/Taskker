using System;
using System.Linq;
using Taskker.Models;
using Taskker.Models.Services;
using System.Web.Mvc;
using Taskker.Models.DAL;
using System.Collections.Generic;
using System.Web.Security;
using System.Threading.Tasks;

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

    /**
     * TODO
     * - hacer mas accesible la carga de horas en task details
     * - mejorar el estilo de task details
     * - Hacer mas accesible la busqueda de grupos
     */

    [Authorize]
    [CustomAuthenticationFilter]
    public class BrowseController : Controller
    {
        private UnitOfWork unitOfWork;
        private readonly NotesService notesService;

        public BrowseController()
        {
            this.unitOfWork = new UnitOfWork();
            notesService = new NotesService();
        }

        [HttpGet]
        public async Task<ActionResult> Index(string grupo)
        {
            UserSession userSession = Session["UserSession"] as UserSession;

            if (userSession is null)
                return RedirectToAction("Login", "Auth");

            // Obtenemos todas las notas desde la API
            ViewData["Notes"] = await notesService.GetNotes(userSession.ID);

            // Buscar todos los grupos a los que pertenece el usuario para mostrarlos en el navbar
            // Si no tiene grupos redireccionar a Groups
            List<Grupo> grupos = null;
            try 
            {
                Usuario loggedUser = unitOfWork.UsuarioRepository.Get(u => u.Email == userSession.Email).SingleOrDefault();

                if (loggedUser == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                List<string> roles = new List<string>();

                // Obtenemos los roles para la vista
                loggedUser.Roles.ToList().ForEach(
                        rol => roles.Add(rol.Nombre)
                );

                ViewData["Roles"] = roles;

                // Obtenemos los grupos que el usuario creo o es miembro
                grupos = loggedUser.Grupos.Concat(loggedUser.CreatedGroups).Distinct().ToList();

                // Si no esta en ningun grupo es redirigido al index para unirse o crear un grupo
                if (grupos.Count == 0)
                    return RedirectToAction("Index", "Groups");

                List<string> nombresGrupos = new List<string>();
                grupos.ForEach(g => nombresGrupos.Add(g.Nombre));

                Session["Grupos"] = nombresGrupos;
                List<Tarea> tareasList;

                Grupo currentGroup;

                // Chequeamos cual sera el grupo activo
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

        [HttpPost]
        [AuthorizeRoleAttribute("Project Manager")]
        public ActionResult DeleteTask(int id)
        {
            unitOfWork.TareaRepository.Delete(id);
            unitOfWork.Save();

            return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<bool> DeleteNote(string id)
        {
            bool result = await notesService.DeleteNote(id);
            return result;
        }

        [HttpGet]
        public async Task<bool> UpdateNote(string id, string text)
        {
            UserSession us = Session["UserSession"] as UserSession;
            Note updated = new Note() { closed = false, created_by = us.ID, text = text, _id = id };
            bool result = await notesService.UpdateNote(updated);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNote(string text)
        {
            UserSession us = Session["UserSession"] as UserSession;
            await notesService.Create(text, us.ID);
            return RedirectToAction("Index", "Browse");
        }

        [HttpPost]
        [AuthorizeRoleAttribute("Project Manager")]
        public ActionResult CreateTask(TareaModel t)
        {
            // Parseamos el tiempo
            DateTime timeEstimated = DateTime.Parse(t.Estimado);
            //Utils.parseTime(t.Estimado);

            // Obtenemos los usuarios asignados a la tarea
            if (t.Asignees == null)
            {
                t.Asignees = "";
            }

            List<string> asignees = new List<string>(t.Asignees.Split(','));
            List<Usuario> asigneesToAdd = new List<Usuario>();
            asignees.ForEach(nombre =>
            {
                // Obtenemos el usuario en base al nombre
                var found = from user in unitOfWork
                                         .UsuarioRepository
                                         .Get(u => nombre == u.NombreApellido)
                            select user;

                try
                {
                    // Si no existe el usuario no lo agregamos
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

        public List<Rol> GetAllRoles()
        {
            List<Rol> roles = unitOfWork.RolRepository.Get().ToList();
            return roles;
        } 

        [HttpGet]
        [AuthorizeRoleAttribute("Project Manager")]
        public ActionResult ControlPanel()
        {
            ViewData["roles"] = this.GetAllRoles();
            Grupo current = (Grupo)Session["CurrentGroup"];
            return PartialView("_ControlPanel", current);
        }

        [HttpPost]
        [AuthorizeRoleAttribute("Project Manager")]
        public ActionResult ControlPanel(List<ControlPanelModel> usuarios)
        {

            foreach(var updatedUser in usuarios)
            {

                List<Rol> allRoles = this.GetAllRoles();

                Usuario userFound = unitOfWork.UsuarioRepository.GetByID(updatedUser.ID);

                List<Rol> newRoles = new List<Rol>();

                foreach(var r in updatedUser.Roles)
                {
                    var rFound = allRoles.FirstOrDefault(rl => rl.Nombre == r);
                    newRoles.Add(rFound);
                }

                userFound.Roles.Clear();
                userFound.Roles = newRoles;

                if(TryUpdateModel(userFound, new string[] { "Roles" }))
                {
                    unitOfWork.Save();
                }
            }

            return RedirectToAction("Index", "Browse");
        }

        [HttpPost]
        public ActionResult UpdateTask(TareaModel tm)
        {

            try
            {
                // Obtenemos la tarea que se debe actualizar
                var tareaFound = unitOfWork.TareaRepository.GetByID(tm.Id);

                List<string> usuarios = new List<string>();

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
                List<Usuario> usersToDelete = new List<Usuario>();

                // Usuarios a agregar
                foreach (var usrStr in usuarios)
                {
                    try
                    {
                        var userfound = from u in unitOfWork.UsuarioRepository
                                            .Get(_us => _us.NombreApellido == usrStr)
                                        select u;

                        Usuario usuario = userfound.Single();

                        if (!tareaFound.Usuarios.Contains(usuario))
                        {
                            filteredUsuarios.Add(usuario);
                        }

                    }
                    catch (InvalidOperationException) { }
                }

                // Usuarios a eliminar
                foreach(var usr in tareaFound.Usuarios)
                {
                    var result = from userName in usuarios
                                 where usr.NombreApellido.ToLower() == userName.ToLower()
                                 select userName;

                    if (result.SingleOrDefault() == null)
                    {
                        usersToDelete.Add(usr);
                    }
                }

                // Eliminamos cada usuario
                usersToDelete.ForEach(u => tareaFound.Usuarios.Remove(u));

                TimeTracked tiempo = null;
                bool newTrackedTime = false;

                try
                {
                    // Buscamos el time tracked
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
                        unitOfWork.Save();

                        newTrackedTime = true;
                    }
                }

                tareaFound.Descripcion = tm.Descripcion;
                tareaFound.Titulo = tm.Titulo;

                foreach(var usr in filteredUsuarios)
                {
                    tareaFound.Usuarios.Add(usr);
                }

                tareaFound.Estimado = Utils.parseTime(tm.Estimado);

                if (tm.TiempoRegistrado != null && !newTrackedTime)
                {
                    DateTime parsedTime = Utils.parseTime(tm.TiempoRegistrado);

                    tiempo.Time = tiempo.Time
                        .AddHours(parsedTime.Hour)
                        .AddMinutes(parsedTime.Minute)
                        .AddSeconds(parsedTime.Second);
                    
                    if (TryUpdateModel(tiempo, new string[] { "Time" }))
                    {
                        unitOfWork.Save();
                    }
                }

                if (tm.Tipo == null)
                    tareaFound.Tipo = TareaTipo.SinTipo;
                else
                    tareaFound.Tipo = (TareaTipo)Enum.Parse(
                        typeof(TareaTipo),
                        tm.Tipo
                    );

                unitOfWork.Save();
            }
            catch (InvalidOperationException){}

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            Dictionary<string, string> UserNamePhoto = this.CreateUsersDict(
                unitOfWork.UsuarioRepository.Get().ToList()
            );

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

                UserSession us = Session["UserSession"] as UserSession;
                Usuario found = unitOfWork.UsuarioRepository.GetByID(us.ID);
                List<string> viewRoles = new List<string>();
                found.Roles.ToList().ForEach(r => viewRoles.Add(r.Nombre));
                ViewData["Roles"] = viewRoles;

                return PartialView("TaskDetails", display);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
        }
    }
}