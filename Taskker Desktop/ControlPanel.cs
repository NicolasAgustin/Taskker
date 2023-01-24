using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taskker_Desktop.Models;
using Taskker_Desktop.Models.DAL;

namespace Taskker_Desktop
{
    public partial class ControlPanel : Form
    {
        public ControlPanel()
        {
            InitializeComponent();

            List<Usuario> allUsuarios = Context.unitOfWork.UsuarioRepository.Get().ToList();
            List<Rol> allRoles = Context.unitOfWork.RolRepository.Get().ToList();
            List<Grupo> allGroups = Context.unitOfWork.GrupoRepository.Get().ToList();

            foreach (var user in allUsuarios)
            {
                var control = new CustomControl(user, allRoles, allGroups);
                panel.Controls.Add(control);
            }
        }

        private void confirmarBtn_Click(object sender, EventArgs e)
        {
            foreach (var ctrl in panel.Controls)
            {
                try
                {
                    CustomControl ctrlCustom = (CustomControl)ctrl;
                    ControlData data = ctrlCustom.ObtainCheckedData();
                    var usr = Context.unitOfWork.UsuarioRepository.GetByID(data.IDUsuario);

                    usr.Roles = data.Roles;
                    usr.Grupos = data.Grupos;
                    Context.unitOfWork.Save();
                }
                catch (Exception)
                {
                    continue;
                }
            }

            exitoLabel.Text = "Cambios guardados";
            var dt = DateTime.Now.AddSeconds(6);
            System.Threading.Timer timer = new System.Threading.Timer(
                (obj) => {
                    if (!this.IsDisposed)
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            exitoLabel.Text = "";
                        }));
                    }
                },
                null,
                dt - DateTime.Now,
                TimeSpan.FromHours(24)
            );

        }
    }
}
