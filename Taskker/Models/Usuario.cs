//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Taskker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public int usuarioId { get; set; }
        public string nombre_apellido { get; set; }
        public string email { get; set; }
        public string profile_picture_path { get; set; }
        public byte[] e_password { get; set; }
    }
}
