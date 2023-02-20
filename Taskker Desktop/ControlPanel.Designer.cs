
namespace Taskker_Desktop
{
    partial class ControlPanel
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
            this.panel = new System.Windows.Forms.FlowLayoutPanel();
            this.confirmarBtn = new System.Windows.Forms.Button();
            this.exitoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Location = new System.Drawing.Point(12, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(786, 426);
            this.panel.TabIndex = 0;
            // 
            // confirmarBtn
            // 
            this.confirmarBtn.Location = new System.Drawing.Point(307, 451);
            this.confirmarBtn.Name = "confirmarBtn";
            this.confirmarBtn.Size = new System.Drawing.Size(145, 23);
            this.confirmarBtn.TabIndex = 1;
            this.confirmarBtn.Text = "Confirmar cambios";
            this.confirmarBtn.UseVisualStyleBackColor = true;
            this.confirmarBtn.Click += new System.EventHandler(this.confirmarBtn_Click);
            // 
            // exitoLabel
            // 
            this.exitoLabel.AutoSize = true;
            this.exitoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitoLabel.ForeColor = System.Drawing.Color.White;
            this.exitoLabel.Location = new System.Drawing.Point(533, 451);
            this.exitoLabel.Name = "exitoLabel";
            this.exitoLabel.Size = new System.Drawing.Size(0, 20);
            this.exitoLabel.TabIndex = 2;
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(810, 486);
            this.Controls.Add(this.exitoLabel);
            this.Controls.Add(this.confirmarBtn);
            this.Controls.Add(this.panel);
            this.Name = "ControlPanel";
            this.Text = "ControlPanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panel;
        private System.Windows.Forms.Button confirmarBtn;
        private System.Windows.Forms.Label exitoLabel;
    }
}