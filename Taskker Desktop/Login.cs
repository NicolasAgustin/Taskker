using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Taskker_Desktop.Models;
using Taskker_Desktop.Models.DAL;

namespace Taskker_Desktop
{
    public partial class Login : Form
    {
        private UnitOfWork unitOfWork;
        public Login()
        {
            unitOfWork = new UnitOfWork();
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            string insertedEmail = email.Text;
            string insertedPassword = password.Text;

            insertedEmail = "nicolas.a.sandez@gmail.com";
            insertedPassword = "pass123";

            byte[] hashed_pass = Utils.HashPassword(insertedPassword);

            try
            {
                
                var address = new MailAddress(insertedEmail).Address;

                var user = from u in unitOfWork.UsuarioRepository.Get(
                    us => us.Email == address && hashed_pass == us.EncptPassword
                )
                           select u;

                Usuario loggedUser = user.SingleOrDefault();

                if (loggedUser == null)
                {
                    emailTip.ToolTipTitle = "Error de ingreso";
                    emailTip.Show("El correo electronico no existe", email);
                    return;
                }

                UserSession.setUserData(loggedUser);

                if (loggedUser.EncodedProfilePicture == null)
                {
                    loggedUser.EncodedProfilePicture =
                        Utils.EncodeFromBitmap(
                            Properties.Resources._default
                        );

                    unitOfWork.Save();
                }

                UserSession.EncodedPicture = loggedUser.EncodedProfilePicture;

                var frm = new Home();
                frm.Location = this.Location;
                frm.StartPosition = FormStartPosition.Manual;
                frm.FormClosing += delegate { this.Show(); };
                frm.Show();
                this.Hide();

            } catch (FormatException)
            {
                emailTip.ToolTipTitle = "Correo electronico invalido";
                emailTip.Show("Ingresa un correo electronico valido", email);
            }

            Console.WriteLine(insertedEmail + insertedPassword);
        }

        private void registerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var regFrm = new Register();
            regFrm.Location = this.Location;
            regFrm.StartPosition = FormStartPosition.Manual;
            regFrm.FormClosing += delegate { this.Show(); };
            regFrm.Show();
            this.Hide();
        }
    }
}
