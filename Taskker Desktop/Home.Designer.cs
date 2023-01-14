
namespace Taskker_Desktop
{
    partial class Home
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.tareas = new System.Windows.Forms.ListView();
            this.salirLink = new System.Windows.Forms.LinkLabel();
            this.perfilLink = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.grupos = new System.Windows.Forms.GroupBox();
            this.nombreLabel = new System.Windows.Forms.Label();
            this.crearTareaBtn = new System.Windows.Forms.Button();
            this.reporteBtn = new System.Windows.Forms.Button();
            this.panelBtn = new System.Windows.Forms.Button();
            this.fotoPerfil = new System.Windows.Forms.PictureBox();
            this.gruposList = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.grupos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fotoPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1312, 86);
            this.panel1.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.splitContainer1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 86);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1116, 572);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(150, 0);
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "TASKKER";
            // 
            // tareas
            // 
            this.tareas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tareas.ForeColor = System.Drawing.SystemColors.Menu;
            this.tareas.HideSelection = false;
            this.tareas.Location = new System.Drawing.Point(162, 114);
            this.tareas.Name = "tareas";
            this.tareas.Size = new System.Drawing.Size(1150, 544);
            this.tareas.TabIndex = 4;
            this.tareas.UseCompatibleStateImageBehavior = false;
            this.tareas.SelectedIndexChanged += new System.EventHandler(this.tareas_SelectedIndexChanged_1);
            // 
            // salirLink
            // 
            this.salirLink.AutoSize = true;
            this.salirLink.Location = new System.Drawing.Point(9, 264);
            this.salirLink.Name = "salirLink";
            this.salirLink.Size = new System.Drawing.Size(27, 13);
            this.salirLink.TabIndex = 6;
            this.salirLink.TabStop = true;
            this.salirLink.Text = "Salir";
            this.salirLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.salirLink_LinkClicked);
            // 
            // perfilLink
            // 
            this.perfilLink.AutoSize = true;
            this.perfilLink.Location = new System.Drawing.Point(95, 264);
            this.perfilLink.Name = "perfilLink";
            this.perfilLink.Size = new System.Drawing.Size(30, 13);
            this.perfilLink.TabIndex = 7;
            this.perfilLink.TabStop = true;
            this.perfilLink.Text = "Perfil";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 328);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // grupos
            // 
            this.grupos.Controls.Add(this.gruposList);
            this.grupos.Location = new System.Drawing.Point(3, 357);
            this.grupos.Name = "grupos";
            this.grupos.Size = new System.Drawing.Size(150, 157);
            this.grupos.TabIndex = 10;
            this.grupos.TabStop = false;
            this.grupos.Text = "Grupos";
            // 
            // nombreLabel
            // 
            this.nombreLabel.AutoSize = true;
            this.nombreLabel.Location = new System.Drawing.Point(16, 227);
            this.nombreLabel.Name = "nombreLabel";
            this.nombreLabel.Size = new System.Drawing.Size(0, 13);
            this.nombreLabel.TabIndex = 11;
            // 
            // crearTareaBtn
            // 
            this.crearTareaBtn.Location = new System.Drawing.Point(162, 89);
            this.crearTareaBtn.Name = "crearTareaBtn";
            this.crearTareaBtn.Size = new System.Drawing.Size(105, 22);
            this.crearTareaBtn.TabIndex = 12;
            this.crearTareaBtn.Text = "Crear tarea";
            this.crearTareaBtn.UseVisualStyleBackColor = true;
            this.crearTareaBtn.Click += new System.EventHandler(this.crearTareaBtn_Click);
            // 
            // reporteBtn
            // 
            this.reporteBtn.Location = new System.Drawing.Point(273, 89);
            this.reporteBtn.Name = "reporteBtn";
            this.reporteBtn.Size = new System.Drawing.Size(105, 22);
            this.reporteBtn.TabIndex = 13;
            this.reporteBtn.Text = "Generar reporte";
            this.reporteBtn.UseVisualStyleBackColor = true;
            // 
            // panelBtn
            // 
            this.panelBtn.Location = new System.Drawing.Point(384, 89);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Size = new System.Drawing.Size(105, 22);
            this.panelBtn.TabIndex = 14;
            this.panelBtn.Text = "Panel de control";
            this.panelBtn.UseVisualStyleBackColor = true;
            // 
            // fotoPerfil
            // 
            this.fotoPerfil.Location = new System.Drawing.Point(12, 95);
            this.fotoPerfil.Name = "fotoPerfil";
            this.fotoPerfil.Size = new System.Drawing.Size(119, 99);
            this.fotoPerfil.TabIndex = 5;
            this.fotoPerfil.TabStop = false;
            // 
            // gruposList
            // 
            this.gruposList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.gruposList.ForeColor = System.Drawing.SystemColors.Menu;
            this.gruposList.FullRowSelect = true;
            this.gruposList.HideSelection = false;
            this.gruposList.Location = new System.Drawing.Point(1, 19);
            this.gruposList.MultiSelect = false;
            this.gruposList.Name = "gruposList";
            this.gruposList.Size = new System.Drawing.Size(149, 132);
            this.gruposList.TabIndex = 0;
            this.gruposList.UseCompatibleStateImageBehavior = false;
            this.gruposList.SelectedIndexChanged += new System.EventHandler(this.gruposList_SelectedIndexChanged);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 658);
            this.Controls.Add(this.panelBtn);
            this.Controls.Add(this.reporteBtn);
            this.Controls.Add(this.crearTareaBtn);
            this.Controls.Add(this.nombreLabel);
            this.Controls.Add(this.grupos);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.perfilLink);
            this.Controls.Add(this.salirLink);
            this.Controls.Add(this.fotoPerfil);
            this.Controls.Add(this.tareas);
            this.Controls.Add(this.panel1);
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grupos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fotoPerfil)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView tareas;
        private System.Windows.Forms.PictureBox fotoPerfil;
        private System.Windows.Forms.LinkLabel salirLink;
        private System.Windows.Forms.LinkLabel perfilLink;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox grupos;
        private System.Windows.Forms.Label nombreLabel;
        private System.Windows.Forms.Button crearTareaBtn;
        private System.Windows.Forms.Button reporteBtn;
        private System.Windows.Forms.Button panelBtn;
        private System.Windows.Forms.ListView gruposList;
    }
}