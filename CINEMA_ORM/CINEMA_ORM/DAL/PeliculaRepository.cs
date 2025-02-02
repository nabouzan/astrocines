using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class PeliculaRepository : GenericRepository<Pelicula>
    {
        public PeliculaRepository(CinemaContext context) : base(context)
        {

        }
    }
}
