using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Taskker.Models;
using System.Collections.Generic;

namespace Taskker.Controllers
{
    // Este controlador tiene que estar segurizado
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}