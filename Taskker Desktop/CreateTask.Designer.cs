
namespace Taskker_Desktop
{
    partial class CreateTask
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
            this.label5 = new System.Windows.Forms.Label();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.crear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.estimado = new System.Windows.Forms.DateTimePicker();
            this.tipo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.titulo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.asignees = new System.Windows.Forms.CheckedListBox();
            this.tituloToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(59, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 29);
            this.label5.TabIndex = 24;
            this.label5.Text = "Descripcion";
            // 
            // descripcion
            // 
            this.descripcion.Location = new System.Drawing.Point(360, 202);
            this.descripcion.Multiline = true;
            this.descripcion.Name = "descripcion";
            this.descripcion.Size = new System.Drawing.Size(295, 151);
            this.descripcion.TabIndex = 23;
            // 
            // crear
            // 
            this.crear.Location = new System.Drawing.Point(206, 570);
            this.crear.Name = "crear";
            this.crear.Size = new System.Drawing.Size(320, 31);
            this.crear.TabIndex = 21;
            this.crear.Text = "Crear";
            this.crear.UseVisualStyleBackColor = true;
            this.crear.Click += new System.EventHandler(this.crear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(59, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 29);
            this.label3.TabIndex = 18;
            this.label3.Text = "Tiempo Estimado";
            // 
            // estimado
            // 
            this.estimado.Location = new System.Drawing.Point(360, 154);
            this.estimado.Name = "estimado";
            this.estimado.Size = new System.Drawing.Size(166, 20);
            this.estimado.TabIndex = 17;
            // 
            // tipo
            // 
            this.tipo.FormattingEnabled = true;
            this.tipo.Location = new System.Drawing.Point(360, 106);
            this.tipo.Name = "tipo";
            this.tipo.Size = new System.Drawing.Size(166, 21);
            this.tipo.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(59, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 29);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tipo";
            // 
            // titulo
            // 
            this.titulo.Location = new System.Drawing.Point(360, 45);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(166, 20);
            this.titulo.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(59, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "Titulo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(59, 397);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 29);
            this.label4.TabIndex = 26;
            this.label4.Text = "Asignar";
            // 
            // asignees
            // 
            this.asignees.CheckOnClick = true;
            this.asignees.FormattingEnabled = true;
            this.asignees.Location = new System.Drawing.Point(360, 397);
            this.asignees.Name = "asignees";
            this.asignees.ScrollAlwaysVisible = true;
            this.asignees.Size = new System.Drawing.Size(295, 94);
            this.asignees.TabIndex = 28;
            this.asignees.SelectedIndexChanged += new System.EventHandler(this.asignees_SelectedIndexChanged);
            // 
            // CreateTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(800, 637);
            this.Controls.Add(this.asignees);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.descripcion);
            this.Controls.Add(this.crear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.estimado);
            this.Controls.Add(this.tipo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.titulo);
            this.Controls.Add(this.label1);
            this.Name = "CreateTask";
            this.Text = "CreateTask";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox descripcion;
        private System.Windows.Forms.Button crear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker estimado;
        private System.Windows.Forms.ComboBox tipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox titulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox asignees;
        private System.Windows.Forms.ToolTip tituloToolTip;
    }
}