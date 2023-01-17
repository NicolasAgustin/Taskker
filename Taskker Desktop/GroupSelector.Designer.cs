
namespace Taskker_Desktop
{
    partial class GroupSelector
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gruposDisponibles = new System.Windows.Forms.CheckedListBox();
            this.unirseBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nombreGrupo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.usuariosDisponibles = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.crearBtn = new System.Windows.Forms.Button();
            this.grupoError = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1142, 465);
            this.panel2.TabIndex = 7;
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
            this.panel4.Location = new System.Drawing.Point(587, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(509, 389);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.unirseBtn);
            this.panel3.Controls.Add(this.gruposDisponibles);
            this.panel3.Location = new System.Drawing.Point(47, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(480, 389);
            this.panel3.TabIndex = 0;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(0, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1142, 86);
            this.panel1.TabIndex = 6;
            // 
            // gruposDisponibles
            // 
            this.gruposDisponibles.CheckOnClick = true;
            this.gruposDisponibles.FormattingEnabled = true;
            this.gruposDisponibles.Location = new System.Drawing.Point(93, 158);
            this.gruposDisponibles.Name = "gruposDisponibles";
            this.gruposDisponibles.ScrollAlwaysVisible = true;
            this.gruposDisponibles.Size = new System.Drawing.Size(295, 94);
            this.gruposDisponibles.TabIndex = 29;
            // 
            // unirseBtn
            // 
            this.unirseBtn.Location = new System.Drawing.Point(128, 322);
            this.unirseBtn.Name = "unirseBtn";
            this.unirseBtn.Size = new System.Drawing.Size(212, 23);
            this.unirseBtn.TabIndex = 30;
            this.unirseBtn.Text = "Unirse";
            this.unirseBtn.UseVisualStyleBackColor = true;
            this.unirseBtn.Click += new System.EventHandler(this.unirseBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(98, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 37);
            this.label2.TabIndex = 31;
            this.label2.Text = "Unirse a un grupo";
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
            // nombreGrupo
            // 
            this.nombreGrupo.Location = new System.Drawing.Point(190, 151);
            this.nombreGrupo.Name = "nombreGrupo";
            this.nombreGrupo.Size = new System.Drawing.Size(254, 20);
            this.nombreGrupo.TabIndex = 33;
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
            // GroupSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 545);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "GroupSelector";
            this.Text = "GroupSelector";
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button unirseBtn;
        private System.Windows.Forms.CheckedListBox gruposDisponibles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox usuariosDisponibles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nombreGrupo;
        private System.Windows.Forms.Button crearBtn;
        private System.Windows.Forms.ToolTip grupoError;
    }
}