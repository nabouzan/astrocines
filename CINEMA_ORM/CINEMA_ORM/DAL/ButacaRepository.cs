using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class ButacaRepository : GenericRepository<Butaca>
    {
        public ButacaRepository(CinemaContext context) : base(context)
        {

        }
    }
}
