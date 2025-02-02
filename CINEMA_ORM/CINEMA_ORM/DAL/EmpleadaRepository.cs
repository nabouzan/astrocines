using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class EmpleadaRepository : GenericRepository<Empleada>
    {
        public EmpleadaRepository(CinemaContext context) : base(context)
        {

        }
        public Empleada EmpleadaCompleta(string correo, string pass)
        {
            return Single(e => e.Correo.Equals(correo) && e.Clave.Equals(pass));
        }

    }
}
