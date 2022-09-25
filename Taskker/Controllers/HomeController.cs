using System.Web.Mvc;
using Taskker.Models;

namespace Taskker.Controllers
{
    // Este controlador tiene que estar segurizado
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserSession userSession = Session["UserSession"] as UserSession;

            if (userSession != null)
                return RedirectToAction("Index", "Browse");

            return View();
        }
    }
}