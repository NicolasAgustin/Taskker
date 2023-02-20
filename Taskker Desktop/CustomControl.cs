using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taskker_Desktop.Models;
using Taskker_Desktop.Models.DAL;
using System.Collections.Generic;

namespace Taskker_Desktop
{
    public partial class CustomControl : UserControl
    {
        private Usuario userToDisplay;
        private List<Rol> AllRoles;
        private List<Grupo> AllGroups;
        public CustomControl(Usuario usuario, List<Rol> allRoles, List<Grupo> allGroups)
        {
            InitializeComponent();
            userToDisplay = usuario;
            nombre.Text = usuario.NombreApellido;
            AllRoles = allRoles;
            AllGroups = allGroups;
            InitializeGroups(allGroups);
            InitializeRoles(allRoles);
        }

        public ControlData ObtainCheckedData()
        {
            List<string> checkedRoles = new List<string>();
            List<string> checkedGroups = new List<string>();
            foreach (object obj in roles.CheckedItems)
            {
                checkedRoles.Add(obj.ToString());
            }

            foreach (object obj in grupos.CheckedItems)
            {
                checkedGroups.Add(obj.ToString());
            }

            List<Rol> checkedRolesResult = (from rol in AllRoles
                                           where checkedRoles.Contains(rol.Nombre)
                                           select rol).ToList();

            List<Grupo> checkedGroupsResult = (
                from grupo in AllGroups
                where checkedGroups.Contains(grupo.Nombre) || grupo.UsuarioID == userToDisplay.ID
                select grupo
            ).ToList();

            return new ControlData() { 
                Grupos = checkedGroupsResult, 
                Roles = checkedRolesResult, 
                IDUsuario = userToDisplay.ID
            };
        }

        private void InitializeGroups(List<Grupo> allGroups)
        {

            allGroups.ForEach(grupo =>
            {
                grupos.Items.Add(grupo.Nombre);
                int index = grupos.FindStringExact(grupo.Nombre);
                if (userToDisplay.CreatedGroups.Concat(userToDisplay.Grupos).ToList().Any(
                    g => g.Nombre == grupo.Nombre || grupo.UsuarioID == userToDisplay.ID))
                {
                    grupos.SetItemChecked(index, true);
                }
            });
            
        }

        private void InitializeRoles(List<Rol> allRoles)
        {
            allRoles.ForEach(rol =>
            {
                roles.Items.Add(rol.Nombre);
                int index = roles.FindStringExact(rol.Nombre);
                if (userToDisplay.Roles.ToList().Any(g => g.Nombre == rol.Nombre))
                {
                    roles.SetItemChecked(index, true);
                }
            });
        }
    }
}
