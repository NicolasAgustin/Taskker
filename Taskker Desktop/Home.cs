using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private Grupo GrupoActual { get; set; }
        public Home()
        {
            // TODO:
            // - Agregar boton para eliminar tarea

            InitializeComponent();

            if (RedirectToGroupSwitcher())
            {
                var frm = new GroupSelector();
                frm.Location = Location;
                frm.StartPosition = FormStartPosition.Manual;
                frm.FormClosing += delegate { Show(); };
                frm.Show();
                Hide();
                Close();
                return;
            }

            if (!Context.unitOfWork.UsuarioRepository.GetByID(UserSession.ID)
                .Roles.Any(r => r.Nombre == "Project Manager"))
            {
                panelBtn.Hide();
                crearTareaBtn.Hide();
            }

            guardarReporte.RestoreDirectory = true;
            guardarReporte.Filter = "Text files (*.txt)|*.txt|CSV (delimitado por comas) (*.csv)|*.csv";

            nombreLabel.AutoSize = false;
            nombreLabel.TextAlign = ContentAlignment.MiddleCenter;
            nombreLabel.Dock = DockStyle.Fill;

            Image profilePicture = Utils.ImageFromBase64(UserSession.EncodedPicture);
            fotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;
            fotoPerfil.Image = profilePicture;
            initializeTaskList();
            initializeGroupList();

            List<Grupo> gruposDisponibles = Context.unitOfWork.GrupoRepository.Get(
                g => g.Usuarios.Any(u => u.ID == UserSession.ID) || g.Usuario.ID == UserSession.ID
            ).ToList();
            
            // Es seguro acceder a la posicion 0
            // La validacion de si no hay grupos se hace mas arriba
            GrupoActual = gruposDisponibles[0];
        }

        private bool RedirectToGroupSwitcher()
        {
            Usuario currentUser = Context.unitOfWork.UsuarioRepository.GetByID(UserSession.ID);

            if (currentUser.Grupos == null)
            {
                return true;
            }

            return ((currentUser.Grupos.Count + currentUser.CreatedGroups.Count) == 0);
        }
        private void initializeGroupList()
        {
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

            var tareaSelected = Context.unitOfWork.TareaRepository.Get(
                t => t.Titulo == titulo
            );

            Tarea toDisplay = tareaSelected.SingleOrDefault();

            if (toDisplay == null)
            {
                return;
            }

            var details = new TaskDetails(toDisplay);
            details.Location = Location;
            details.StartPosition = FormStartPosition.Manual;
            details.FormClosing += delegate { 
                this.Show();
                Refresh();
                Reload();
            };
            details.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Reload();
        }

        public void Reload()
        {
            // Hay que chequear si el usuario tiene los mismos datos
            // hace falta traerse el usuario desde el unitofwork y actualizar
            // los datos desde ahi
            var currentUser = Context.unitOfWork.UsuarioRepository.GetByID(UserSession.ID);

            // Hay que chequear si el usuario sigue estando en el grupo
            if (!(currentUser.Grupos.Any(g => g.ID == GrupoActual.ID) || currentUser.CreatedGroups.Any(
                gc => gc.ID == GrupoActual.ID)))
            {
                // Aca revisamos si el usuario sigue teniendo grupos que podemos mostrar
                // Si no tiene ningun grupo entonces hay que redirigir al formulario para unirse o crear
                if (RedirectToGroupSwitcher())
                {
                    var frm = new GroupSelector();
                    frm.Location = Location;
                    frm.StartPosition = FormStartPosition.Manual;
                    frm.FormClosing += delegate { Show(); };
                    frm.Show();
                    Hide();
                    Close();
                    return;
                } else
                {
                    List<Grupo> gruposRestantes = Context.unitOfWork.GrupoRepository.Get(
                        g => g.Usuarios.Any(u => u.ID == UserSession.ID) || g.Usuario.ID == UserSession.ID
                    ).ToList();

                    GrupoActual = gruposRestantes[0];
                }
            }

            // Actualizamos la foto de perfil
            // Asegurar que usersession tenga info actualizada
            Image profilePicture = Utils.ImageFromBase64(currentUser.EncodedProfilePicture);
            fotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;
            fotoPerfil.Image = profilePicture;

            // Actualizamos el nombre del usuario
            nombreLabel.Text = currentUser.NombreApellido;

            // Actualizamos las tareas
            tareas.Items.Clear();
            tareas.AccessibilityObject.ToString();
            List<Tarea> tareasFound = Context.unitOfWork.TareaRepository.Get(
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

            List<Grupo> grupos = Context.unitOfWork.GrupoRepository.Get(
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

            var grupo = Context.unitOfWork.GrupoRepository.Get(
                g => g.Nombre == grupoSelected).SingleOrDefault();

            if (grupo == null)
                return;

            // Ver como hacer para cambiar de grupo
            GrupoActual = grupo;
            Reload();
        }

        private void reporteBtn_Click(object sender, EventArgs e)
        {

            Usuario currentUser = Context.unitOfWork.UsuarioRepository.GetByID(UserSession.ID);

            // Buscamos todos los registros de tiempo para el usuario actual
            List<TimeTracked> tiemposRegistrados = Context.unitOfWork.TtrackedRepository.Get(
                tt => tt.Usuario.ID == currentUser.ID
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
                            tr => tr.UsuarioID == currentUser.ID
                        ).Time.TimeOfDay.TotalHours.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
                    )
                )
            );

            // Creamos un stream a partir de la tabla convertida a string
            Stream stream = Utils.GenerateStreamFromString(
                Utils.CreateCSVDataTable(report)
            );

            if (guardarReporte.ShowDialog() == DialogResult.OK)
            {
                string outputDirectory = guardarReporte.FileName;
                using (var fileStream = File.Create(outputDirectory))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
        }

        private void panelBtn_Click(object sender, EventArgs e)
        {
            var frm = new ControlPanel();
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate {
                Show();
                Refresh();
                Reload();
            };
            frm.Show();
        }

        private void perfilLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new Profile();
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate {
                Show();
                Refresh();
                Reload();
            };
            frm.Show();
        }

        private void createGroupBtn_Click(object sender, EventArgs e)
        {
            var frm = new CreateGroup();
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate
            {
                Show();
                Refresh();
                Reload();
            };
            frm.Show();
        }

        private void unirseGroupBtn_Click(object sender, EventArgs e)
        {
            var frm = new JoinGroup();
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate
            {
                Show();
                Refresh();
                Reload();
            };
            frm.Show();
        }
    }
}
