
namespace Taskker_Desktop
{
    partial class CreateGroup
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.crearBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.usuariosDisponibles = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nombreGrupo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grupoError = new System.Windows.Forms.ToolTip(this.components);
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel4.Controls.Add(this.crearBtn);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.usuariosDisponibles);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.nombreGrupo);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(1, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(509, 389);
            this.panel4.TabIndex = 2;
            // 
            // crearBtn
            // 
            this.crearBtn.Location = new System.Drawing.Point(171, 322);
            this.crearBtn.Name = "crearBtn";
            this.crearBtn.Size = new System.Drawing.Size(212, 23);
            this.crearBtn.TabIndex = 32;
            this.crearBtn.Text = "Crear grupo";
            this.crearBtn.UseVisualStyleBackColor = true;
            this.crearBtn.Click += new System.EventHandler(this.crearBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(80, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 24);
            this.label6.TabIndex = 36;
            this.label6.Text = "usuarios";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(80, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 24);
            this.label5.TabIndex = 35;
            this.label5.Text = "Agregar";
            // 
            // usuariosDisponibles
            // 
            this.usuariosDisponibles.CheckOnClick = true;
            this.usuariosDisponibles.FormattingEnabled = true;
            this.usuariosDisponibles.Location = new System.Drawing.Point(190, 204);
            this.usuariosDisponibles.Name = "usuariosDisponibles";
            this.usuariosDisponibles.ScrollAlwaysVisible = true;
            this.usuariosDisponibles.Size = new System.Drawing.Size(254, 94);
            this.usuariosDisponibles.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(79, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 24);
            this.label4.TabIndex = 34;
            this.label4.Text = "Nombre";
            // 
            // nombreGrupo
            // 
            this.nombreGrupo.Location = new System.Drawing.Point(190, 151);
            this.nombreGrupo.Name = "nombreGrupo";
            this.nombreGrupo.Size = new System.Drawing.Size(254, 20);
            this.nombreGrupo.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(164, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 37);
            this.label3.TabIndex = 32;
            this.label3.Text = "Crear grupo";
            // 
            // CreateGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 388);
            this.Controls.Add(this.panel4);
            this.Name = "CreateGroup";
            this.Text = "CreateGroup";
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button crearBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox usuariosDisponibles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nombreGrupo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip grupoError;
    }
}