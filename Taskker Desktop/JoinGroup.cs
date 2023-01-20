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
    public partial class JoinGroup : Form
    {
        public JoinGroup()
        {
            InitializeComponent();
            LoadGrupos();
        }
        public void LoadGrupos()
        {
            // Deberia filtrarse por el grupo
            List<Grupo> grupos = Context.unitOfWork.GrupoRepository.Get().ToList();
            Usuario currentUser = Context.unitOfWork.UsuarioRepository.GetByID(UserSession.ID);

            List<string> displayNames = new List<string>();

            grupos.ForEach(u => displayNames.Add(u.Nombre));

            gruposDisponibles.Items.AddRange(displayNames.ToArray());

            currentUser.Grupos.Concat(currentUser.CreatedGroups).ToList().ForEach(g =>
            {
                var name = g.Nombre;
                int index = gruposDisponibles.FindStringExact(name);
                if (index != -1)
                    gruposDisponibles.SetItemChecked(index, true);
            });
        }

        private void unirseBtn_Click(object sender, EventArgs e)
        {
            List<Grupo> grupos = new List<Grupo>();
            Usuario currentUser = Context.unitOfWork.UsuarioRepository.GetByID(UserSession.ID);
            // Traerme todos los items marcados en asignees
            foreach (var item in gruposDisponibles.CheckedItems)
            {

                var grp = Context.unitOfWork.GrupoRepository.Get(
                    u => u.Nombre == item.ToString()
                ).SingleOrDefault();

                if (grp == null || currentUser.CreatedGroups.Any(gc => gc.ID == grp.ID))
                    continue;

                grupos.Add(grp);
            }

            currentUser.Grupos = grupos;
            Context.unitOfWork.Save();

            Close();
        }
    }
}
