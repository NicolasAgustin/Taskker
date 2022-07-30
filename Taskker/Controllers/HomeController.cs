using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Taskker.Models;
using System.Collections.Generic;

namespace Taskker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Database db = new Database();

            var lista = db.Grupoes.ToList();

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = lista
            };
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