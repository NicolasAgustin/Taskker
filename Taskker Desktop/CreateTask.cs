using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taskker_Desktop.Models;
using Taskker_Desktop.Models.DAL;

namespace Taskker_Desktop
{
    public partial class CreateTask : Form
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        private int GrupoToAdd { get; set; }
        public CreateTask(int grupoID)
        {

            GrupoToAdd = grupoID;
            InitializeComponent();
            LoadAsignees();

            estimado.Format = DateTimePickerFormat.Time;
            estimado.ShowUpDown = true;
            estimado.Value = new DateTime(2000, 1, 1);

            tipo.Items.Add(TareaTipo.Desarrollo);
            tipo.Items.Add(TareaTipo.Tarea);
            tipo.Items.Add(TareaTipo.SinTipo);

            // Revisar si hace falta recibir por parametro la instancia del formulario Home
        }

        public void LoadAsignees()
        {
            // Deberia filtrarse por el grupo
            List<Usuario> usuarios = unitOfWork.UsuarioRepository.Get(
                u => u.Grupos.Any(gp => gp.ID == GrupoToAdd) 
                || u.CreatedGroups.Any(gc => gc.ID == GrupoToAdd)
            ).ToList();

            List<string> displayNames = new List<string>();

            usuarios.ForEach(u => displayNames.Add(u.NombreApellido));

            asignees.Items.AddRange(displayNames.ToArray());
        }

        private void asignees_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void crear_Click(object sender, EventArgs e)
        {
            List<Usuario> asignados = new List<Usuario>();
            // Traerme todos los items marcados en asignees
            foreach(var item in asignees.CheckedItems)
            {

                var usr = unitOfWork.UsuarioRepository.Get(u => u.NombreApellido == item.ToString()).SingleOrDefault();
                
                if (usr == null)
                    continue;

                asignados.Add(usr);
            }

            // Traer toda la info de los demas controles del formulario
            if (unitOfWork.TareaRepository.Get(t => t.Titulo == titulo.Text).SingleOrDefault() != null)
            {
                tituloToolTip.ToolTipTitle = "Ya se encuentra creada una tarea con el mismo titulo.";
                tituloToolTip.Show("Ya se encuentra creada una tarea con el mismo titulo.", titulo);
                return;
            }

            Tarea nueva = new Tarea();
            nueva.Titulo = titulo.Text;
            nueva.Tipo = (TareaTipo)Enum.Parse(typeof(TareaTipo), tipo.SelectedItem.ToString());
            nueva.Estimado = estimado.Value;
            nueva.Descripcion = descripcion.Text;
            nueva.Usuarios = asignados;
            nueva.GrupoID = GrupoToAdd;

            unitOfWork.TareaRepository.Insert(nueva);
            // Falta resolver el feature de grupos
            unitOfWork.Save();

        }
    }
}
