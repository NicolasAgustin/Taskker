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
    public partial class ControlPanel : Form
    {
        private UnitOfWork unitOfWork;
        public ControlPanel()
        {
            unitOfWork = new UnitOfWork();
            InitializeComponent();

            List<Usuario> allUsuarios = unitOfWork.UsuarioRepository.Get().ToList();
            List<Rol> allRoles = unitOfWork.RolRepository.Get().ToList();
            List<Grupo> allGroups = unitOfWork.GrupoRepository.Get().ToList();

            foreach (var user in allUsuarios)
            {
                var control = new CustomControl(user, allRoles, allGroups);
                panel.Controls.Add(control);
            }
        }

        private void confirmarBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
