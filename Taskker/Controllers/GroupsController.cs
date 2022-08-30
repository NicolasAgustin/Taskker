using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taskker.Models;

namespace Taskker.Controllers
{
    [Authorize]
    [CustomAuthenticationFilter]
    public class GroupsController : Controller
    {
        // GET: Groups
        public ActionResult Index()
        {
            return View();
        }
    }
}