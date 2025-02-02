using CINEMA_ORM.Modelo;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class RolRepository : GenericRepository<Rol>
    {
        public RolRepository(CinemaContext context) : base(context)
        {

        }
        public Rol RolCompleto(int rolId)
        {
            return Get(r => r.RolId == rolId, includeProperties: "Empleadas").FirstOrDefault();
        }
    }
}
