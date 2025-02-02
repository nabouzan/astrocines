using System;
using System.Collections.Generic;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class UnidadDeTrabajo : IDisposable
    {
        private CinemaContext context = new CinemaContext();
        private bool disposed = false;


        private CineRepository cineRepository;
        private EmpleadaRepository empleadaRepository;
        private RolRepository rolRepository;
        private SalaRepository salaRepository;
        private ButacaRepository butacaRepository;
        private EntradaRepository entradaRepository;
        private SesionRepository sesionRepository;
        private HorarioRepository horarioRepository;
        private PeliculaRepository peliculaRepository;

        public UnidadDeTrabajo()
        {
            context.Database.EnsureCreated();
        }

        public CineRepository CineRepository
        {
            get
            {
                if (cineRepository == null)
                {
                    cineRepository = new CineRepository(context);
                }

                return cineRepository;
            }
        }

        public EmpleadaRepository EmpleadaRepository
        {
            get
            {
                if (empleadaRepository == null)
                {
                    empleadaRepository = new EmpleadaRepository(context);
                }

                return empleadaRepository;
            }
        }

        public RolRepository RolRepository
        {
            get
            {
                if (rolRepository == null)
                {
                    rolRepository = new RolRepository(context);
                }

                return rolRepository;
            }
        }

        public SalaRepository SalaRepository
        {
            get
            {
                if (salaRepository == null)
                {
                    salaRepository = new SalaRepository(context);
                }

                return salaRepository;
            }
        }

        public ButacaRepository ButacaRepository
        {
            get
            {
                if (butacaRepository == null)
                {
                    butacaRepository = new ButacaRepository(context);
                }

                return butacaRepository;
            }
        }
        public EntradaRepository EntradaRepository
        {
            get
            {
                if (entradaRepository == null)
                {
                    entradaRepository = new EntradaRepository(context);
                }

                return entradaRepository;
            }
        }
        public SesionRepository SesionRepository
        {
            get
            {
                if (sesionRepository == null)
                {
                    sesionRepository = new SesionRepository(context);
                }

                return sesionRepository;
            }
        }
        public HorarioRepository HorarioRepository
        {
            get
            {
                if (horarioRepository == null)
                {
                    horarioRepository = new HorarioRepository(context);
                }

                return horarioRepository;
            }
        }
        public PeliculaRepository PeliculaRepository
        {
            get
            {
                if (peliculaRepository == null)
                {
                    peliculaRepository = new PeliculaRepository(context);
                }

                return peliculaRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
