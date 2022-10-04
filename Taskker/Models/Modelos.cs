using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Taskker.Models
{
    public class GrupoModel
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string nombre { get; set; }
    }

    public class RegistroModel
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string Apellido { get; set; }
        
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electronico no es valido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public HttpPostedFileBase Photo { set; get; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electronico no es valido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class JoinGroupModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre { get; set; }
    }

    public class CreateGroupModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre { get; set; }
    }

    public class TareaModel
    {
        [Required(ErrorMessage = "El campo Titulo es obligatorio.")]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Asignees { get; set; }
        public string Estimado { get; set; }
        public string Tipo { get; set; }
        public int Id { get; set; }
        public string TiempoRegistrado { get; set; }
    }
}