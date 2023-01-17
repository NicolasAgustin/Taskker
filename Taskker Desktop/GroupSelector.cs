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
    public partial class GroupSelector : Form
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public GroupSelector()
        {
            InitializeComponent();
            LoadAsignees();
            LoadGrupos();
        }

        public void LoadGrupos()
        {
            // Deberia filtrarse por el grupo
            List<Grupo> usuarios = unitOfWork.GrupoRepository.Get().ToList();

            List<string> displayNames = new List<string>();

            usuarios.ForEach(u => displayNames.Add(u.Nombre));

            gruposDisponibles.Items.AddRange(displayNames.ToArray());
        }

        public void LoadAsignees()
        {
            // Deberia filtrarse por el grupo
            List<Usuario> usuarios = unitOfWork.UsuarioRepository.Get(
                grp => grp.ID != UserSession.ID
                ).ToList();

            List<string> displayNames = new List<string>();

            usuarios.ForEach(u => displayNames.Add(u.NombreApellido));

            usuariosDisponibles.Items.AddRange(displayNames.ToArray());
        }

        private void unirseBtn_Click(object sender, EventArgs e)
        {
            List<Grupo> grupos = new List<Grupo>();
            // Traerme todos los items marcados en asignees
            foreach (var item in gruposDisponibles.CheckedItems)
            {

                var grp = unitOfWork.GrupoRepository.Get(u => u.Nombre == item.ToString()).SingleOrDefault();

                if (grp == null)
                    continue;

                grupos.Add(grp);
            }

            Usuario currentUser = unitOfWork.UsuarioRepository.GetByID(UserSession.ID);

            currentUser.Grupos = grupos;
            unitOfWork.Save();

            RedirectToHome();
        }

        private void RedirectToHome()
        {
            var frm = new Home();
            frm.Location = Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { Show(); };
            if (!frm.IsDisposed)
            {
                frm.Show();
            }

            Hide();
        }

        private void crearBtn_Click(object sender, EventArgs e)
        {
            List<Usuario> usuarios = new List<Usuario>();

            Usuario currentUser = unitOfWork.UsuarioRepository.GetByID(UserSession.ID);

            string grupoNuevo = nombreGrupo.Text;

            if (unitOfWork.GrupoRepository.Get().ToList().Any(g => g.Nombre.ToLower() == grupoNuevo.ToLower()))
            {
                grupoError.ToolTipTitle = "El nombre del grupo ya se encuentra utilizado.";
                grupoError.Show("El nombre del grupo ya se encuentra utilizado.", nombreGrupo);
                return;
            }

            foreach (var item in usuariosDisponibles.CheckedItems)
            {

                var usr = unitOfWork.UsuarioRepository.Get(
                    u => u.NombreApellido == item.ToString()).SingleOrDefault();

                if (usr == null)
                    continue;

                usuarios.Add(usr);
            }

            Grupo nuevo = new Grupo() { 
                Nombre = grupoNuevo, 
                UsuarioID = UserSession.ID,
                Usuarios = usuarios
            };

            currentUser.CreatedGroups.Add(nuevo);

            unitOfWork.GrupoRepository.Insert(nuevo);
            unitOfWork.Save();

            RedirectToHome();
        }
    }
}
