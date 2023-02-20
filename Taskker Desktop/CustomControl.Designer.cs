
namespace Taskker_Desktop
{
    partial class CustomControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.grupos = new System.Windows.Forms.CheckedListBox();
            this.roles = new System.Windows.Forms.CheckedListBox();
            this.nombre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // grupos
            // 
            this.grupos.CheckOnClick = true;
            this.grupos.FormattingEnabled = true;
            this.grupos.Location = new System.Drawing.Point(26, 73);
            this.grupos.Name = "grupos";
            this.grupos.ScrollAlwaysVisible = true;
            this.grupos.Size = new System.Drawing.Size(150, 94);
            this.grupos.TabIndex = 29;
            // 
            // roles
            // 
            this.roles.CheckOnClick = true;
            this.roles.FormattingEnabled = true;
            this.roles.Location = new System.Drawing.Point(182, 73);
            this.roles.Name = "roles";
            this.roles.ScrollAlwaysVisible = true;
            this.roles.Size = new System.Drawing.Size(150, 94);
            this.roles.TabIndex = 30;
            // 
            // nombre
            // 
            this.nombre.AutoSize = true;
            this.nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombre.Location = new System.Drawing.Point(26, 28);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(57, 20);
            this.nombre.TabIndex = 31;
            this.nombre.Text = "label1";
            // 
            // CustomControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.roles);
            this.Controls.Add(this.grupos);
            this.Name = "CustomControl";
            this.Size = new System.Drawing.Size(359, 188);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox grupos;
        private System.Windows.Forms.CheckedListBox roles;
        private System.Windows.Forms.Label nombre;
    }
}
