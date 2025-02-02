using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class Rol
    {
        public Rol()
        {
            Empleadas = new HashSet<Empleada>();
        }

        public int RolId { get; set; }
        public string NombreRol { get; set; }
        //COLECCIÓN DE EMPLEADAS
        public virtual ICollection<Empleada> Empleadas { get; set; }
    }
}
