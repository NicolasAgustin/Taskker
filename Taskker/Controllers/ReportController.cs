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

        /// <summary>
        /// Accion para descargar el reporte de horas unicamente para el usuario
        /// logeado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileStreamResult DownloadReport()
        {
            UserSession us = (UserSession)Session["UserSession"];

            var user = from u in unitOfWork.UsuarioRepository.Get(_user => _user.Email == us.Email)
                       select u;

            Usuario userFound = user.Single();

            // Buscamos todos los registros de tiempo para el usuario actual
            List<TimeTracked> tiemposRegistrados = unitOfWork.TtrackedRepository.Get(
                tt => tt.Usuario.ID == userFound.ID
            ).ToList();

            List<Tarea> tareas = new List<Tarea>();

            tiemposRegistrados.ForEach(
                tr => tareas.Add(tr.Tarea)
            );

            // Creamos una tabla
            DataTable report = new DataTable();

            // Agregamos los headers
            report.Columns.Add("Grupo", typeof(string));
            report.Columns.Add("Tarea", typeof(string));
            report.Columns.Add("Descripcion", typeof(string));
            report.Columns.Add("Tiempo", typeof(string));

            // Agrupamos las tareas por grupo
            var groupedTasks = tareas
                .GroupBy(t => t.GrupoID)
                .Select(g => g.ToList())
                .ToList();

            // Por cada tarea agregamos una fila a la tabla
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

            // Creamos un stream a partir de la tabla convertida a string
            Stream stream = Utils.GenerateStreamFromString(
                Utils.CreateCSVDataTable(report)
            );

            var fileStreamResult = new FileStreamResult(stream, "text/csv");
            fileStreamResult.FileDownloadName = "reporte.csv";

            return fileStreamResult;
        }
    }
}