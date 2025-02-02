using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CINEMA_ORM.DAL
{
    public class CineRepository : GenericRepository<Cine>
    {
        public CineRepository(CinemaContext context) : base(context)
        {

        }
        public Cine CineCompleto(int cineId)
        {
            return Get(c => c.CineId == cineId, includeProperties: "Empleadas").FirstOrDefault();
        }
    }
}
