using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Taskker.Models
{
    public class GrupoModel
    {
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

    public class TareaModel
    {
        [Required(ErrorMessage = "El campo Titulo es obligatorio.")]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Asignees { get; set; }
        public string Tipo { get; set; }

    }
}