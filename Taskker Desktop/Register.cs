using System;
using System.IO;
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
    public partial class Register : Form
    {
        private Usuario Nuevo { get; set; }
        private UnitOfWork unitOfWork { get; set; }
        public Register()
        {
            unitOfWork = new UnitOfWork();
            InitializeComponent();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cargarFoto_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                fotoCargada.Text = openFileDialog1.FileName;
                Stream fileUploaded = openFileDialog1.OpenFile();
                string encoded = Utils.EncodeFromStream(fileUploaded);
                UserSession.EncodedPicture = encoded;
            } else
            {
                UserSession.EncodedPicture = null;
            }
        }

        private void registrarse_Click(object sender, EventArgs e)
        {
            var res = unitOfWork.UsuarioRepository.Get(u => u.Email == email.Text);
            if(res.SingleOrDefault() == null)
            {
                Rol defaultRole = unitOfWork.RolRepository.Get(r => r.Nombre == "Desarrollador").FirstOrDefault();
                Usuario nuevo = new Usuario();
                nuevo.Roles = new List<Rol>() { defaultRole };
                nuevo.Email = email.Text;
                nuevo.Nombre = nombre.Text;
                nuevo.Apellido = apellido.Text;
                nuevo.Discriminator = "Usuario";
                nuevo.EncptPassword = Utils.HashPassword(password.Text);
                if (UserSession.EncodedPicture == null)
                {
                    nuevo.EncodedProfilePicture = 
                        Utils.EncodeFromBitmap(
                            Properties.Resources._default
                        );

                    UserSession.EncodedPicture = nuevo.EncodedProfilePicture;
                }
                else
                {
                    nuevo.EncodedProfilePicture = UserSession.EncodedPicture;
                }

                unitOfWork.UsuarioRepository.Insert(nuevo);
                unitOfWork.Save();

                UserSession.setUserData(nuevo);
                var frm = new Home();
                frm.Location = Location;
                frm.StartPosition = FormStartPosition.Manual;
                frm.FormClosing += delegate { Show(); };
                frm.Show();
                Hide();
            } else
            {
                errorTip.ToolTipTitle = "Correo electronico ya se encuentra en uso.";
                errorTip.Show("Correo electronico ya se encuentra en uso.", email);
            }
            
        }
    }
}
