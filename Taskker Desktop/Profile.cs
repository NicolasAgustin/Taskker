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
    public partial class Profile : Form
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Usuario Displayed;
        public Profile()
        {
            InitializeComponent();
            Displayed = unitOfWork.UsuarioRepository.GetByID(UserSession.ID);
            nombre.Text = Displayed.Nombre;
            apellido.Text = Displayed.Apellido;
            email.Text = Displayed.Email;

            // Implementar la carga y modificacion de la foto de perfil

            foreach (var grupo in Displayed.Grupos.ToList())
            {
                var item = new ListViewItem(
                    new string[] {
                        grupo.Nombre
                    }
                );

                grupos.Items.Add(item);
            }

            foreach (var grupo in Displayed.CreatedGroups.ToList())
            {
                var item = new ListViewItem(
                    new string[] {
                        grupo.Nombre
                    }
                );

                gruposCreados.Items.Add(item);
            }

            foreach (var rol in Displayed.Roles.ToList())
            {
                var item = new ListViewItem(
                    new string[] {
                        rol.Nombre
                    }
                );

                roles.Items.Add(item);
            }
        }

    }
}
