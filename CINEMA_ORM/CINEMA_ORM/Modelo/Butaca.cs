using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class Butaca
    {
        public Butaca()
        {
            Entradas = new HashSet<SesionEntradaButaca>();
        }

        public int ButacaId { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Nº Fila")]
        public int NFila { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Nº Butaca")]
        public int NButaca { get; set; }
        //public Byte[] Disponible { get; set; }
        //public Byte[] Ocupada { get; set; }

        //PROPIEDAD DE NAVEGACIÓN
        public virtual Sala Sala { get; set; }
        public int Sala_SalaId { get; set; }

        //COLECCIÓN DE ENTRADAS
        public virtual ICollection<SesionEntradaButaca> Entradas { get; set; }
    }
}
