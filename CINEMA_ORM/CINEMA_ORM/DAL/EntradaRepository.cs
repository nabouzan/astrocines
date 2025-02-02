using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class EntradaRepository : GenericRepository<SesionEntradaButaca>
    {
        public EntradaRepository(CinemaContext context) : base(context)
        {

        }
    }
}
