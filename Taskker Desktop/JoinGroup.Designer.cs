
namespace Taskker_Desktop
{
    partial class JoinGroup
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.unirseBtn = new System.Windows.Forms.Button();
            this.gruposDisponibles = new System.Windows.Forms.CheckedListBox();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.unirseBtn);
            this.panel3.Controls.Add(this.gruposDisponibles);
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(480, 389);
            this.panel3.TabIndex = 1;
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
            // JoinGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 388);
            this.Controls.Add(this.panel3);
            this.Name = "JoinGroup";
            this.Text = "JoinGroup";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button unirseBtn;
        private System.Windows.Forms.CheckedListBox gruposDisponibles;
    }
}