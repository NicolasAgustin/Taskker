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

            descripcion.Text = Displayed.Descripcion;

            estimado.Format = DateTimePickerFormat.Time;
            estimado.ShowUpDown = true;
            estimado.Value = Displayed.Estimado;

            registrarTiempo.Format = DateTimePickerFormat.Time;
            registrarTiempo.ShowUpDown = true;
            registrarTiempo.Value = new DateTime(2000, 1, 1);

            // Headers de la listview para los tiempos
            tiempos.View = View.Details;
            tiempos.FullRowSelect = true;
            foreach (var prop in new string[] { "Usuario", "Tiempo registrado" })
            {
                tiempos.Columns.Add(prop, 200, HorizontalAlignment.Center);
            }

            // TODO:
            // Falta agregar un control para la descripcion
            DisplayTimesPerUser();
        }

        private void DisplayTimesPerUser()
        {
            List<TimeTracked> tiemposFound = unitOfWork.TtrackedRepository.Get(
                tt => tt.TareaID == Displayed.ID &&
                tt.UsuarioID == UserSession.ID
            ).ToList();

            foreach(var tfound in tiemposFound)
            {
                var item = new ListViewItem(
                    new string[] { 
                        tfound.Usuario.NombreApellido, 
                        tfound.Time.ToString("HH:mm:ss") 
                    }
                );

                tiempos.Items.Add(item);
            }
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

            toUpdate.Descripcion = descripcion.Text;

            TimeTracked time = toUpdate.TiempoRegistrado.SingleOrDefault(
                t => t.UsuarioID == UserSession.ID);

            var regTiempoValue = registrarTiempo.Value;

            registrarTiempo.Value = new DateTime(2000, 1, 1);

            if (regTiempoValue.ToString("HH:mm:ss") == "00:00:00")
            {
                unitOfWork.Save();
                return;
            }

            if (time == null)
            {
                TimeTracked tt = new TimeTracked()
                {
                    Time = regTiempoValue,
                    TareaID = Displayed.ID,
                    UsuarioID = UserSession.ID
                };

                unitOfWork.TtrackedRepository.Insert(tt);
                toUpdate.TiempoRegistrado.Add(tt);

            } else
            {

                time.Time = time.Time.AddHours(regTiempoValue.Hour)
                    .AddMinutes(regTiempoValue.Minute)
                    .AddSeconds(regTiempoValue.Second);

            }

            unitOfWork.Save();
        }

        private void actualizar_Click(object sender, EventArgs e)
        {
            FormToModel();
            tiempos.Items.Clear();
            tiempos.AccessibilityObject.ToString();
            DisplayTimesPerUser();

        }

    }
}
