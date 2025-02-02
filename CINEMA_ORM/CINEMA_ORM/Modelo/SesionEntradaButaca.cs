using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class SesionEntradaButaca
    {
        //Clave formada por 2 ids
        public int Sesion_SesionId { get; set; }
        public virtual Sesion Sesion { get; set; }

        public int Butaca_ButacaId { get; set; }
        public virtual Butaca Butaca { get; set; }

        [Required(ErrorMessage = "Se requiere el campo de Fecha")]
        [DataType(DataType.Date, ErrorMessage = "Inserte una fecha válida")]
        public DateTime FechaHoraVenta { get; set; }
        //public string Estado { get; set; }
    }
}
