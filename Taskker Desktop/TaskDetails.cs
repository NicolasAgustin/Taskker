using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taskker_Desktop.Models.DAL;
using Taskker_Desktop.Models;


namespace Taskker_Desktop
{
    public partial class TaskDetails : Form
    {
        private Home HomeForm;
        private Tarea Displayed;
        private UnitOfWork unitOfWork;
        public TaskDetails(Tarea toDisplay, Home homeFrm, UnitOfWork unitOfWork)
        {
            InitializeComponent();

            this.unitOfWork = unitOfWork;

            Displayed = toDisplay;

            titulo.Text = toDisplay.Titulo;
            tipo.Items.Add(TareaTipo.Desarrollo);
            tipo.Items.Add(TareaTipo.Tarea);
            tipo.Items.Add(TareaTipo.SinTipo);
            tipo.SelectedIndex = tipo.FindStringExact(
                toDisplay.Tipo.ToString());

            HomeForm = homeFrm;

            estimado.Format = DateTimePickerFormat.Time;
            estimado.ShowUpDown = true;
            estimado.Value = Displayed.Estimado;

            registrarTiempo.Format = DateTimePickerFormat.Time;
            registrarTiempo.ShowUpDown = true;
            registrarTiempo.Value = new DateTime();
        }

        private void TaskDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("Formulario cerrado");
            this.HomeForm.Refresh();
            this.HomeForm.Reload();
        }

        private void FormToModel()
        {
            Tarea toUpdate = unitOfWork.TareaRepository.GetByID(Displayed.ID);

            toUpdate.Titulo = titulo.Text;
            toUpdate.Tipo = (TareaTipo)Enum.Parse(typeof(TareaTipo), tipo.SelectedItem.ToString());
            toUpdate.Estimado = estimado.Value;

            if (!toUpdate.TiempoRegistrado.Any(t => t.Time.ToString("HH:mm:ss") == registrarTiempo.Value.ToString("HH: mm:ss")))
            {
                TimeTracked tt = new TimeTracked()
                {
                    Time = registrarTiempo.Value,
                    TareaID = Displayed.ID,
                    UsuarioID = UserSession.ID
                };

                toUpdate.TiempoRegistrado.Add(tt);
            }

            unitOfWork.Save();
        }

        private void actualizar_Click(object sender, EventArgs e)
        {
            FormToModel();
        }
    }
}
