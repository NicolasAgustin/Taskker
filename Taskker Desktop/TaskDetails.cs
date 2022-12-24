using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taskker_Desktop.Models.DAL;
using Taskker_Desktop.Models;


namespace Taskker_Desktop
{
    public partial class TaskDetails : Form
    {
        public TaskDetails(Tarea toDisplay)
        {
            InitializeComponent();
            titulo.Text = toDisplay.Titulo;
            tipo.Items.Add(TareaTipo.Desarrollo);
            tipo.Items.Add(TareaTipo.Tarea);
            tipo.Items.Add(TareaTipo.SinTipo);
            tipo.SelectedIndex = tipo.FindStringExact(
                toDisplay.Tipo.ToString());
        }
    }
}
