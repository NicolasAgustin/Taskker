using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taskker_Desktop.Models;
using Taskker_Desktop.Models.DAL;

namespace Taskker_Desktop
{
    public partial class Profile : Form
    {
        private Usuario Displayed;
        private string EncodedImage;
        private System.Threading.Timer Timer;
        public Profile()
        {
            InitializeComponent();
            Displayed = Context.unitOfWork.UsuarioRepository.GetByID(UserSession.ID);
            nombre.Text = Displayed.Nombre;
            apellido.Text = Displayed.Apellido;
            email.Text = Displayed.Email;

            EncodedImage = Displayed.EncodedProfilePicture;

            Image profilePicture = Utils.ImageFromBase64(Displayed.EncodedProfilePicture);
            fotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;
            fotoPerfil.Image = profilePicture;

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

        private void guardar_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            var currentUser = Context.unitOfWork.UsuarioRepository.GetByID(Displayed.ID);

            currentUser.Nombre = nombre.Text;
            currentUser.Apellido = apellido.Text;
            currentUser.Email = email.Text;
            currentUser.EncodedProfilePicture = EncodedImage;

            Context.unitOfWork.Save();

            exitoLabel.Text = "Perfil actualizado";
            var dt = DateTime.Now.AddSeconds(6);
            Timer = new System.Threading.Timer(
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

        private void fotoPerfil_Click(object sender, EventArgs e)
        {
            if (seleccionarFoto.ShowDialog() == DialogResult.OK)
            {
                Stream fileUploaded = seleccionarFoto.OpenFile();
                string encoded = Utils.EncodeFromStream(fileUploaded);
                EncodedImage = encoded;
                Image profilePicture = Utils.ImageFromBase64(EncodedImage);
                fotoPerfil.SizeMode = PictureBoxSizeMode.StretchImage;
                fotoPerfil.Image = profilePicture;
            }
        }

        private void Profile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            Timer.Dispose();
        }
    }
}
