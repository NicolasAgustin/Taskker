using System;
using System.Linq;
using System.Windows.Forms;
using Taskker_Desktop.Models;
using Taskker_Desktop.Models.DAL;
using System.Collections.Generic;

namespace Taskker_Desktop
{
    public partial class CreateGroup : Form
    {
        public CreateGroup()
        {
            InitializeComponent();
            LoadAsignees();
        }
        public void LoadAsignees()
        {
            // Deberia filtrarse por el grupo
            List<Usuario> usuarios = Context.unitOfWork.UsuarioRepository.Get(
                grp => grp.ID != UserSession.ID
                ).ToList();

            List<string> displayNames = new List<string>();

            usuarios.ForEach(u => displayNames.Add(u.NombreApellido));

            usuariosDisponibles.Items.AddRange(displayNames.ToArray());
        }

        private void crearBtn_Click(object sender, EventArgs e)
        {
            List<Usuario> usuarios = new List<Usuario>();

            Usuario currentUser = Context.unitOfWork.UsuarioRepository.GetByID(UserSession.ID);

            string grupoNuevo = nombreGrupo.Text;

            if (Context.unitOfWork.GrupoRepository.Get().ToList().Any(g => g.Nombre.ToLower() == grupoNuevo.ToLower()))
            {
                grupoError.ToolTipTitle = "El nombre del grupo ya se encuentra utilizado.";
                grupoError.Show("El nombre del grupo ya se encuentra utilizado.", nombreGrupo);
                return;
            }

            foreach (var item in usuariosDisponibles.CheckedItems)
            {

                var usr = Context.unitOfWork.UsuarioRepository.Get(
                    u => u.NombreApellido == item.ToString()).SingleOrDefault();

                if (usr == null)
                    continue;

                usuarios.Add(usr);
            }

            Grupo nuevo = new Grupo()
            {
                Nombre = grupoNuevo,
                UsuarioID = UserSession.ID,
                Usuarios = usuarios
            };

            currentUser.CreatedGroups.Add(nuevo);

            Context.unitOfWork.GrupoRepository.Insert(nuevo);
            Context.unitOfWork.Save();

            Close();
        }
    }
}
