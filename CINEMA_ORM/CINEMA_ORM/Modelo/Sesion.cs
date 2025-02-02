using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class Sesion
    {
        public Sesion()
        {
            Entradas = new HashSet<SesionEntradaButaca>();
        }

        public int SesionId { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Nombre")]
        [StringLength(maximumLength: 60, ErrorMessage = "Longitud máxima admitida 60 car")]
        public string Nombre { get; set; }

        public virtual Sala Sala { get; set; }
        public int Sala_SalaId { get; set; }

        //COLECCIÓN DE ENTRADAS
        public virtual ICollection<SesionEntradaButaca> Entradas { get; set; }

        //PROPIEDAD DE NAVEGACIÓN
        public virtual Pelicula Pelicula { get; set; }
        public int Pelicula_PeliculaId { get; set; }

        //PROPIEDAD DE NAVEGACIÓN
        public virtual Horario Horario { get; set; }
        public int Horario_HorarioId { get; set; }
    }
}
