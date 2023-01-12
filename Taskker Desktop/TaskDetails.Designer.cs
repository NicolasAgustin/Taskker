
namespace Taskker_Desktop
{
    partial class TaskDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this.titulo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tipo = new System.Windows.Forms.ComboBox();
            this.estimado = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.registrarTiempo = new System.Windows.Forms.DateTimePicker();
            this.actualizar = new System.Windows.Forms.Button();
            this.tiempos = new System.Windows.Forms.ListView();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(38, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Titulo";
            // 
            // titulo
            // 
            this.titulo.Location = new System.Drawing.Point(248, 59);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(166, 20);
            this.titulo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(38, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tipo";
            // 
            // tipo
            // 
            this.tipo.FormattingEnabled = true;
            this.tipo.Location = new System.Drawing.Point(248, 120);
            this.tipo.Name = "tipo";
            this.tipo.Size = new System.Drawing.Size(166, 21);
            this.tipo.TabIndex = 3;
            // 
            // estimado
            // 
            this.estimado.Location = new System.Drawing.Point(248, 168);
            this.estimado.Name = "estimado";
            this.estimado.Size = new System.Drawing.Size(166, 20);
            this.estimado.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(38, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tiempo Estimado";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(38, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "Registrar tiempo";
            // 
            // registrarTiempo
            // 
            this.registrarTiempo.Location = new System.Drawing.Point(248, 220);
            this.registrarTiempo.Name = "registrarTiempo";
            this.registrarTiempo.Size = new System.Drawing.Size(166, 20);
            this.registrarTiempo.TabIndex = 7;
            // 
            // actualizar
            // 
            this.actualizar.Location = new System.Drawing.Point(43, 276);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(112, 31);
            this.actualizar.TabIndex = 8;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = true;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            // 
            // tiempos
            // 
            this.tiempos.HideSelection = false;
            this.tiempos.Location = new System.Drawing.Point(78, 333);
            this.tiempos.Name = "tiempos";
            this.tiempos.Size = new System.Drawing.Size(646, 97);
            this.tiempos.TabIndex = 10;
            this.tiempos.UseCompatibleStateImageBehavior = false;
            // 
            // descripcion
            // 
            this.descripcion.Location = new System.Drawing.Point(450, 92);
            this.descripcion.Multiline = true;
            this.descripcion.Name = "descripcion";
            this.descripcion.Size = new System.Drawing.Size(295, 151);
            this.descripcion.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(445, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Descripcion";
            // 
            // TaskDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.descripcion);
            this.Controls.Add(this.tiempos);
            this.Controls.Add(this.actualizar);
            this.Controls.Add(this.registrarTiempo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.estimado);
            this.Controls.Add(this.tipo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.titulo);
            this.Controls.Add(this.label1);
            this.Name = "TaskDetails";
            this.Text = "TaskDetails";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TaskDetails_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titulo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tipo;
        private System.Windows.Forms.DateTimePicker estimado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker registrarTiempo;
        private System.Windows.Forms.Button actualizar;
        private System.Windows.Forms.ListView tiempos;
        private System.Windows.Forms.TextBox descripcion;
        private System.Windows.Forms.Label label5;
    }
}