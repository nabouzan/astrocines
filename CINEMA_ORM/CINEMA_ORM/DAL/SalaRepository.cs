using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class SalaRepository : GenericRepository<Sala>
    {
        public SalaRepository(CinemaContext context) : base(context)
        {

        }
    }
}
