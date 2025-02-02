using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class Sala
    {
        public Sala()
        {
            Sesiones = new HashSet<Sesion>();
        }

        public int SalaId { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Nombre")]
        [StringLength(maximumLength: 40, ErrorMessage = "Longitud máxima admitida 40 car.")]
        public string Nombre { get; set; }

        //PROPIEDAD DE NAVEGACIÓN
        public virtual Cine Cine { get; set; }
        public int Cine_CineId { get; set; }

        //COLECCIÓN DE SESIONES
        public virtual ICollection<Sesion> Sesiones { get; set; }

        //COLECCIÓN DE BUTACAS
        public virtual ICollection<Butaca> Butacas { get; set; }
    }
}
