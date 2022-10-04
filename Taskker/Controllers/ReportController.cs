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

            List<Tarea> tareas = unitOfWork.TareaRepository.Get(
                    tarea => tarea.Usuarios.Any(u => u.ID == userFound.ID)
                ).ToList();

            DataTable report = new DataTable();

            report.Columns.Add("Grupo", typeof(string));
            report.Columns.Add("Tarea", typeof(string));
            report.Columns.Add("Descripcion", typeof(string));
            // Tiempo deberia ser float, para que el tiempo trackeado
            // aparezca como 1H 5M -> 1.5
            report.Columns.Add("Tiempo", typeof(int));

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
                    ).Time.Hour
                )
            ));

            Stream stream = Utils.GenerateStreamFromString(
                Utils.CreateCSVDataTable(report)
            );

            var fileStreamResult = new FileStreamResult(stream, "text/csv");
            fileStreamResult.FileDownloadName = "reporte.csv";

            return fileStreamResult;
        }
    }
}