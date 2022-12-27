using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taskker_Desktop.Models;
using Taskker_Desktop.Models.DAL;

namespace Taskker_Desktop
{
    public partial class Home : Form
    {
        private UnitOfWork unitOfWork;
        public Home()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            initializeTaskList();
        }

        private void initializeTaskList()
        {
            tareas.View = View.Details;
            tareas.FullRowSelect = true;
            foreach(var prop in new string[] { "Titulo", "Tipo", "Estimado" })
            {
                tareas.Columns.Add(prop, 200, HorizontalAlignment.Center);
            }
        }

        private void tareas_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (tareas.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem item = tareas.SelectedItems[0];
            string titulo = item.SubItems[0].Text;
            string tipo = item.SubItems[1].Text;
            string estimado = item.SubItems[2].Text;

            var tareaSelected = unitOfWork.TareaRepository.Get(
                t => t.Titulo == titulo
            );

            Tarea toDisplay = tareaSelected.SingleOrDefault();

            if (toDisplay == null)
            {
                return;
            }

            var details = new TaskDetails(toDisplay, this, unitOfWork);
            details.Location = this.Location;
            details.StartPosition = FormStartPosition.Manual;
            details.FormClosing += delegate { this.Show(); };
            details.Show();

            Console.WriteLine("Seleccionado: " + titulo + " " + tipo + " " + estimado);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Reload();
        }

        public void Reload()
        {
            tareas.Items.Clear();

            tareas.AccessibilityObject.ToString();

            List<Tarea> tareasFound = unitOfWork.TareaRepository.Get().ToList();

            foreach (var tarea in tareasFound)
            {
                var item = new ListViewItem(new string[] { tarea.Titulo, tarea.Tipo.ToString(), tarea.Estimado.ToString() });
                tareas.Items.Add(item);
            }
        }
    }
}
