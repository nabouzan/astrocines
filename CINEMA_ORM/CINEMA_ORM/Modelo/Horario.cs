using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class Horario
    {
        public Horario()
        {
            Sesiones = new HashSet<Sesion>();
        }

        public int HorarioId { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Hora")]
        public string Hora { get; set; }

        //COLECCIÓN DE SESIONES
        public virtual ICollection<Sesion> Sesiones { get; set; }
    }
}
