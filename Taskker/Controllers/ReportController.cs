using System.IO;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Taskker.Models;
using Taskker.Models.DAL;
using System.Collections.Generic;

namespace Taskker.Controllers
{
    [Authorize]
    [CustomAuthenticationFilter]
    public class ReportController : Controller
    {
        private UnitOfWork unitOfWork;

        public ReportController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public FileStreamResult DownloadReport()
        {
            UserSession us = (UserSession)Session["UserSession"];

            var user = from u in unitOfWork.UsuarioRepository.Get(_user => _user.Email == us.Email)
                       select u;

            Usuario userFound = user.Single();

            List<TimeTracked> tiemposRegistrados = unitOfWork.TtrackedRepository.Get(
                tt => tt.Usuario.ID == userFound.ID
            ).ToList();

            List<Tarea> tareas = new List<Tarea>();

            tiemposRegistrados.ForEach(
                tr => tareas.Add(tr.Tarea)
            );

            DataTable report = new DataTable();

            report.Columns.Add("Grupo", typeof(string));
            report.Columns.Add("Tarea", typeof(string));
            report.Columns.Add("Descripcion", typeof(string));
            // Tiempo deberia ser float, para que el tiempo trackeado
            // aparezca como 1H 5M -> 1.5
            report.Columns.Add("Tiempo", typeof(string));

            var groupedTasks = tareas
                .GroupBy(t => t.GrupoID)
                .Select(g => g.ToList())
                .ToList();

            groupedTasks.ForEach(group => group.ForEach(
                    task => report.Rows.Add(
                        task.Grupo.Nombre,
                        task.Titulo,
                        task.Descripcion,
                        task.TiempoRegistrado.Single(
                            tr => tr.UsuarioID == userFound.ID
                        ).Time.TimeOfDay.TotalHours.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
                    )
                )
            );

            Stream stream = Utils.GenerateStreamFromString(
                Utils.CreateCSVDataTable(report)
            );

            var fileStreamResult = new FileStreamResult(stream, "text/csv");
            fileStreamResult.FileDownloadName = "reporte.csv";

            return fileStreamResult;
        }
    }
}