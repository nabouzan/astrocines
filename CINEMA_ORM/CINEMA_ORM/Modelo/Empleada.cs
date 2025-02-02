using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class Empleada
    {
     
        public int EmpleadaId { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Nombre")]
        [StringLength(maximumLength: 30, ErrorMessage = "Longitud máxima admitida 30 car")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Apellidos")]
        [StringLength(maximumLength: 60, ErrorMessage = "Longitud máxima admitida 60 car")]
        public string Apellidos { get; set; }
        [StringLength(maximumLength: 60, ErrorMessage = "Longitud máxima admitida 60 car")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Se requiere el campo DNI")]
        [RegularExpression(@"^((([A-Za-z])\d{8})|(\d{8}([A-Za-z])))$", ErrorMessage = "Dni con formato erróneo")]
        public string DNI { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Correo")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido")]
        public string Correo { get; set; }
        [Phone(ErrorMessage = "Formato de teléfono no válido")]
        public string Telefono { get; set; }
        public string Clave { get; set; }

        //PROPIEDAD DE NAVEGACIÓN
        public virtual Cine Cine { get; set; }
        public int Cine_CineId { get; set; }
        //PROPIEDAD DE NAVEGACIÓN
        public virtual Rol Rol { get; set; }
        public int Rol_RolId { get; set; }
      
    }
}
