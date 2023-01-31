
namespace Taskker_Desktop
{
    partial class Profile
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
            this.fotoPerfil = new System.Windows.Forms.PictureBox();
            this.nombre = new System.Windows.Forms.TextBox();
            this.apellido = new System.Windows.Forms.TextBox();
            this.email = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.roles = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.grupos = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.gruposCreados = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.guardar = new System.Windows.Forms.Button();
            this.exitoLabel = new System.Windows.Forms.Label();
            this.seleccionarFoto = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.fotoPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // fotoPerfil
            // 
            this.fotoPerfil.Location = new System.Drawing.Point(282, 44);
            this.fotoPerfil.Name = "fotoPerfil";
            this.fotoPerfil.Size = new System.Drawing.Size(238, 213);
            this.fotoPerfil.TabIndex = 6;
            this.fotoPerfil.TabStop = false;
            this.fotoPerfil.Click += new System.EventHandler(this.fotoPerfil_Click);
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(282, 296);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(363, 20);
            this.nombre.TabIndex = 7;
            // 
            // apellido
            // 
            this.apellido.Location = new System.Drawing.Point(281, 360);
            this.apellido.Name = "apellido";
            this.apellido.Size = new System.Drawing.Size(364, 20);
            this.apellido.TabIndex = 8;
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(282, 421);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(363, 20);
            this.email.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(113, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 29);
            this.label5.TabIndex = 25;
            this.label5.Text = "Nombre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(113, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 29);
            this.label1.TabIndex = 26;
            this.label1.Text = "Apellido";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(113, 412);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 29);
            this.label2.TabIndex = 27;
            this.label2.Text = "Email";
            // 
            // roles
            // 
            this.roles.HideSelection = false;
            this.roles.Location = new System.Drawing.Point(281, 478);
            this.roles.Name = "roles";
            this.roles.Size = new System.Drawing.Size(364, 76);
            this.roles.TabIndex = 28;
            this.roles.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(113, 478);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 29);
            this.label3.TabIndex = 29;
            this.label3.Text = "Roles";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(113, 596);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 29);
            this.label4.TabIndex = 30;
            this.label4.Text = "Grupos";
            // 
            // grupos
            // 
            this.grupos.HideSelection = false;
            this.grupos.Location = new System.Drawing.Point(282, 596);
            this.grupos.Name = "grupos";
            this.grupos.Size = new System.Drawing.Size(364, 72);
            this.grupos.TabIndex = 31;
            this.grupos.UseCompatibleStateImageBehavior = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(113, 708);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 29);
            this.label6.TabIndex = 32;
            this.label6.Text = "Grupos";
            // 
            // gruposCreados
            // 
            this.gruposCreados.HideSelection = false;
            this.gruposCreados.Location = new System.Drawing.Point(281, 708);
            this.gruposCreados.Name = "gruposCreados";
            this.gruposCreados.Size = new System.Drawing.Size(364, 72);
            this.gruposCreados.TabIndex = 33;
            this.gruposCreados.UseCompatibleStateImageBehavior = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(113, 737);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 29);
            this.label7.TabIndex = 34;
            this.label7.Text = "creados";
            // 
            // guardar
            // 
            this.guardar.Location = new System.Drawing.Point(238, 809);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(337, 34);
            this.guardar.TabIndex = 35;
            this.guardar.Text = "Guardar cambios";
            this.guardar.UseVisualStyleBackColor = true;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // exitoLabel
            // 
            this.exitoLabel.AutoSize = true;
            this.exitoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitoLabel.ForeColor = System.Drawing.Color.White;
            this.exitoLabel.Location = new System.Drawing.Point(584, 9);
            this.exitoLabel.Name = "exitoLabel";
            this.exitoLabel.Size = new System.Drawing.Size(0, 20);
            this.exitoLabel.TabIndex = 36;
            // 
            // Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(800, 855);
            this.Controls.Add(this.exitoLabel);
            this.Controls.Add(this.guardar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gruposCreados);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grupos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.roles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.email);
            this.Controls.Add(this.apellido);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.fotoPerfil);
            this.Name = "Profile";
            this.Text = "Profile";
            ((System.ComponentModel.ISupportInitialize)(this.fotoPerfil)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fotoPerfil;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.TextBox apellido;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView roles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView grupos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView gruposCreados;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button guardar;
        private System.Windows.Forms.Label exitoLabel;
        private System.Windows.Forms.OpenFileDialog seleccionarFoto;
    }
}