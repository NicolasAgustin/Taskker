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
        private Grupo GrupoActual { get; set; }
        public Home()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            Image profilePicture = Utils.ImageFromBase64(UserSession.EncodedPicture);
            fotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;
            fotoPerfil.Image = profilePicture;
            initializeTaskList();
            initializeGroupList();

            List<Grupo> gruposDisponibles = unitOfWork.GrupoRepository.Get(
                g => g.Usuarios.Any(u => u.ID == UserSession.ID) || g.Usuario.ID == UserSession.ID
            ).ToList();

            if (gruposDisponibles.Count > 0)
            {
                GrupoActual = gruposDisponibles[0];
            } else
            {
                // Redirigir a crear grupo o unirse
            }
        }

        private void initializeGroupList()
        {
            //gruposList.View = View.Details;
            gruposList.Alignment = ListViewAlignment.Top;
            tareas.FullRowSelect = true;
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
            details.StartPosition = FormStartPosition.CenterScreen;
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

            // Actualizamos la foto de perfil
            fotoPerfil.Image = Utils.ImageFromBase64(UserSession.EncodedPicture);

            // Actualizamos el nombre del usuario
            nombreLabel.Text = UserSession.NombreApellido;

            // Actualizamos las tareas
            tareas.Items.Clear();
            tareas.AccessibilityObject.ToString();
            List<Tarea> tareasFound = unitOfWork.TareaRepository.Get(
                t => t.GrupoID == GrupoActual.ID
                ).ToList();


            foreach (var tarea in tareasFound)
            {
                var item = new ListViewItem(new string[] { 
                    tarea.Titulo, tarea.Tipo.ToString(), tarea.Estimado.ToString() });

                tareas.Items.Add(item);
            }

            // Actualizamos los grupos
            gruposList.Items.Clear();
            gruposList.AccessibilityObject.ToString();

            List<Grupo> grupos = unitOfWork.GrupoRepository.Get(
                g => g.Usuarios.Any(u => u.ID == UserSession.ID) || g.Usuario.ID == UserSession.ID
                ).ToList();

            foreach (var grupo in grupos)
            {
                var item = new ListViewItem(new string[] {
                    grupo.Nombre }
                );

                gruposList.Items.Add(item);
            }

        }

        private void salirLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserSession.Clear();
            tareas.Items.Clear();
            var frm = new Login();
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { Show(); };
            frm.Show();
            Hide();
        }

        private void crearTareaBtn_Click(object sender, EventArgs e)
        {
            // Le pasamos el id del grupo actual en donde tiene que agregar la tarea
            var frm = new CreateTask(GrupoActual.ID);
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { 
                Show();
                Refresh();
                Reload();
            };
            frm.Show();
        }

        private void gruposList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string grupoSelected = gruposList.SelectedItems[0].Text;

            var grupo = unitOfWork.GrupoRepository.Get(g => g.Nombre == grupoSelected).SingleOrDefault();

            if (grupo == null)
                return;

            // Ver como hacer para cambiar de grupo
            GrupoActual = grupo;
            Reload();
        }
    }
}
