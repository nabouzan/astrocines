using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class SesionRepository : GenericRepository<Sesion>
    {
        public SesionRepository(CinemaContext context) : base(context)
        {

        }
    }
}
