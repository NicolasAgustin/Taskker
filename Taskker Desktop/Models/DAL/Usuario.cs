using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Taskker_Desktop.Models.DAL
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string NombreApellido
        {
            get
            {
                return Utils.Capitalize(this.Nombre) + " " + Utils.Capitalize(this.Apellido);
            }
            set
            {
                this.Nombre = value.Split(' ')[0];
                this.Apellido = value.Split(' ')[1];
            }
        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string ProfilePicturePath { get; set; }
        public string Discriminator { get; set; }
        public byte[] EncptPassword { get; set; }
        public string EncodedProfilePicture { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<Grupo> CreatedGroups { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
        public virtual ICollection<Rol> Roles { get; set; }
        public virtual ICollection<TimeTracked> TiempoRegistrado { get; set; }
    }
}
