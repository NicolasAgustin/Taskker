
namespace Taskker_Desktop
{
    partial class Register
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.fotoCargada = new System.Windows.Forms.Label();
            this.cargarFoto = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.MaskedTextBox();
            this.registerLink = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.registrarse = new System.Windows.Forms.Button();
            this.nombre = new System.Windows.Forms.MaskedTextBox();
            this.apellido = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 528);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.fotoCargada);
            this.panel3.Controls.Add(this.cargarFoto);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.password);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.email);
            this.panel3.Controls.Add(this.registerLink);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.registrarse);
            this.panel3.Controls.Add(this.nombre);
            this.panel3.Controls.Add(this.apellido);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(190, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(422, 462);
            this.panel3.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(94, 298);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 14;
            this.label7.Text = "Foto de perfil";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // fotoCargada
            // 
            this.fotoCargada.AutoSize = true;
            this.fotoCargada.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fotoCargada.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fotoCargada.Location = new System.Drawing.Point(198, 316);
            this.fotoCargada.Name = "fotoCargada";
            this.fotoCargada.Size = new System.Drawing.Size(0, 21);
            this.fotoCargada.TabIndex = 13;
            // 
            // cargarFoto
            // 
            this.cargarFoto.Location = new System.Drawing.Point(94, 315);
            this.cargarFoto.Name = "cargarFoto";
            this.cargarFoto.Size = new System.Drawing.Size(75, 23);
            this.cargarFoto.TabIndex = 12;
            this.cargarFoto.Text = "Seleccionar";
            this.cargarFoto.UseVisualStyleBackColor = true;
            this.cargarFoto.Click += new System.EventHandler(this.cargarFoto_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(94, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "Contraseña";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(94, 261);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(221, 20);
            this.password.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(94, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "Correo electronico";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(94, 209);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(221, 20);
            this.email.TabIndex = 3;
            // 
            // registerLink
            // 
            this.registerLink.AutoSize = true;
            this.registerLink.Location = new System.Drawing.Point(121, 397);
            this.registerLink.Name = "registerLink";
            this.registerLink.Size = new System.Drawing.Size(171, 13);
            this.registerLink.TabIndex = 7;
            this.registerLink.TabStop = true;
            this.registerLink.Text = "Ya posee un usuario? Ingrese aqui";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(94, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Apellido";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(94, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre";
            // 
            // registrarse
            // 
            this.registrarse.Location = new System.Drawing.Point(169, 422);
            this.registrarse.Name = "registrarse";
            this.registrarse.Size = new System.Drawing.Size(75, 23);
            this.registrarse.TabIndex = 4;
            this.registrarse.Text = "Registrarse";
            this.registrarse.UseVisualStyleBackColor = true;
            this.registrarse.Click += new System.EventHandler(this.registrarse_Click);
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(94, 108);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(221, 20);
            this.nombre.TabIndex = 1;
            // 
            // apellido
            // 
            this.apellido.Location = new System.Drawing.Point(94, 158);
            this.apellido.Name = "apellido";
            this.apellido.Size = new System.Drawing.Size(221, 20);
            this.apellido.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(151, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "Registro";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 86);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "TASKKER";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 611);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Register";
            this.Text = "Register";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel registerLink;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button registrarse;
        private System.Windows.Forms.MaskedTextBox nombre;
        private System.Windows.Forms.MaskedTextBox apellido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox password;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox email;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label fotoCargada;
        private System.Windows.Forms.Button cargarFoto;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolTip errorTip;
    }
}