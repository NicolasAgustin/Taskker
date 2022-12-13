using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taskker.Models;
using Taskker.Models.DAL;
using System.Text.Json;
using System.Web.Helpers;

namespace Taskker.Controllers
{
    [Authorize]
    [CustomAuthenticationFilter]
    public class GroupsController : Controller
    {
        private UnitOfWork unitOfWork;

        public GroupsController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Groups
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Join()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Join(string nombre)
        {
            UserSession us = (UserSession)Session["UserSession"];

            var user = from u in unitOfWork
                       .UsuarioRepository
                       .Get(_user => _user.Email == us.Email)
                       select u;
            try
            {
                Usuario found = user.Single();

                var group = from g in unitOfWork
                                .GrupoRepository
                                .Get(_grp => _grp.Nombre == nombre)
                            select g;

                Grupo gFound = group.Single();
                
                // Chequemos si el usuario ya esta en el grupo
                if(!gFound.Usuarios.Contains(found))
                    gFound.Usuarios.Add(found);

                unitOfWork.Save();
            } catch (InvalidOperationException) {
                ModelState.AddModelError("Error", "El grupo no existe.");
                return View();
            }

            return RedirectToAction("Index", "Browse");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Accion para eliminar un usuario de un grupo
        /// </summary>
        /// <param name="groupid">ID del grupo</param>
        /// <param name="userid">ID del usuario a eliminar</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteUserFromGroup(int groupid, int userid)
        {
            Grupo groupFound = unitOfWork.GrupoRepository.GetByID(groupid);
            var user = from u in groupFound.Usuarios
                       where u.ID == userid
                       select u;
            Usuario userToDelete = user.SingleOrDefault();
            if (userToDelete != null)
            {
                groupFound.Usuarios.Remove(userToDelete);

                // Intentamos actualizar el modelo
                if (TryUpdateModel(groupFound, new string[] { "Usuarios" }))
                {
                    unitOfWork.Save();
                }
            }

            return null;
        }

        [HttpPost]
        public ActionResult Create(GrupoModel gm)
        {
            if (String.IsNullOrEmpty(gm.nombre))
            {
                ModelState.AddModelError("Error", "El nombre del grupo no puede estar vacio.");
                return View();
            }

            var grp = from g in unitOfWork.GrupoRepository.Get(gr => gr.Nombre == gm.nombre)
                      select g;

            UserSession us = (UserSession)Session["UserSession"];

            var user = unitOfWork.UsuarioRepository.GetByID(us.ID);

            try
            {
                grp.Single();

                // Ya existe el grupo
                ModelState.AddModelError("Error", "El grupo ya existe.");
                return View();
            }
            catch (InvalidOperationException)
            {
                
                Grupo nuevo = new Grupo
                {
                    Nombre = gm.nombre,
                    UsuarioID = us.ID
                };

                // Agregamos al usuario creador al grupo
                nuevo.Usuarios = new List<Usuario>() { user };

                // Guardamos el grupo
                unitOfWork.GrupoRepository.Insert(nuevo);

                unitOfWork.Save();
            }

            return RedirectToAction("Index", "Browse");
        }

        /// <summary>
        /// Accion para obtener todos los grupos disponibles (solamente los nombres)
        /// en formato json
        /// </summary>
        /// <returns>Json</returns>
        [HttpGet]
        public ActionResult GetGroups()
        {
            List<string> nombresGrupos = new List<string>();

            unitOfWork
                .GrupoRepository
                .Get()
                .ToList()
                .ForEach(g => nombresGrupos.Add(g.Nombre));
            
            // Serializamos la lista con los nombres de grupos
            var json = JsonSerializer.Serialize<List<string>>(nombresGrupos);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Accion para ver los detalles de un grupo
        /// </summary>
        /// <param name="gname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GroupDetails/{gname:string}")]
        public ActionResult GroupDetails(string gname)
        {
            var group = from g in unitOfWork.GrupoRepository.Get(_grp => _grp.Nombre == gname)
                        select g;

            Grupo groupFound = group.Single();
            return PartialView("GroupDetails", groupFound);
        }
    }
}