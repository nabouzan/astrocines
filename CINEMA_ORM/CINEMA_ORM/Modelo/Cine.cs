using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class Cine
    {
        public Cine()
        {
            Empleadas = new HashSet<Empleada>();
            Salas = new HashSet<Sala>();
        }

        public int CineId { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Nombre")]
        [StringLength(maximumLength: 40, ErrorMessage = "Longitud máxima admitida 40 car.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Se requiere el campo nº de salas")]
        public int Nsalas { get; set; }
        [Required(ErrorMessage = "Se requiere el campo nº de espectadores")]
        public int Nespectadores { get; set; }

        //[Required(ErrorMessage = "Se requiere el campo de Fecha")]
        //[DataType(DataType.Date, ErrorMessage = "Inserte una fecha válida")]
        //public DateTime FechaRegistro { get; set; }
        //public string fechaReg
        //{
        //    get
        //    {
        //        return FechaRegistro.ToString("dd/MM/yyyy");
        //    }
        //}
        //COLECCIÓN DE EMPLEADAS
        public virtual ICollection<Empleada> Empleadas { get; set; }
        //COLECCIÓN DE SALAS
        public virtual ICollection<Sala> Salas { get; set; }
    }
}
