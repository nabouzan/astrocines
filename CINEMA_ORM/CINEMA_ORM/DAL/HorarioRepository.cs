using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class HorarioRepository : GenericRepository<Horario>
    {
        public HorarioRepository(CinemaContext context) : base(context)
        {

        }
    }
}
