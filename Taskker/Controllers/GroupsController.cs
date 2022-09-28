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
        TaskkerContext db = new TaskkerContext();
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetGroups()
        {
            List<string> nombresGrupos = new List<string>();
            db.Grupos.ToList().ForEach(g => nombresGrupos.Add(g.Nombre));
            
            var json = JsonSerializer.Serialize<List<string>>(nombresGrupos);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("GroupDetails/{gname:string}")]
        public ActionResult GroupDetails(string gname)
        {
            var group = from g in db.Grupos
                        where g.Nombre == gname
                        select g;

            Grupo groupFound = group.Single();

            return PartialView("GroupDetails", groupFound);
        }
    }
}