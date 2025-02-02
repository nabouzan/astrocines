using CINEMA_ORM.Modelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text;

namespace CINEMA_ORM.DAL
{
    public class CinemaContext : DbContext
    {
        public DbSet<Cine> Cines { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Empleada> Empleadas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Butaca> Butacas { get; set; }
        public DbSet<SesionEntradaButaca> Entradas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Cinema;Integrated Security=true");
            //optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Cinema;User Id=sa;password=abc123.");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API

            //CLAVE FORÁNEA EMPLEADA-CINE
            //En cascada para poder eliminar el cine con las empleadas
            modelBuilder.Entity<Empleada>().HasOne<Cine>(c => c.Cine).WithMany(e => e.Empleadas).HasForeignKey(c => c.Cine_CineId).OnDelete(DeleteBehavior.Cascade);
            //CLAVE FORÁNEA EMPLEADA-ROL
            modelBuilder.Entity<Empleada>().HasOne<Rol>(r => r.Rol).WithMany(e => e.Empleadas).HasForeignKey(r => r.Rol_RolId).OnDelete(DeleteBehavior.NoAction);

            //CLAVE FORÁNEA SALA-CINE
            modelBuilder.Entity<Sala>().HasOne<Cine>(c => c.Cine).WithMany(s => s.Salas).HasForeignKey(c => c.Cine_CineId).OnDelete(DeleteBehavior.NoAction);

            //CLAVE FORÁNEA BUTACAS-SALA
            modelBuilder.Entity<Butaca>().HasOne<Sala>(sa => sa.Sala).WithMany(b => b.Butacas).HasForeignKey(sa => sa.Sala_SalaId).OnDelete(DeleteBehavior.NoAction);

            //CLAVE FORÁNEA SESION-HORARIO
            modelBuilder.Entity<Sesion>().HasOne<Horario>(h => h.Horario).WithMany(se => se.Sesiones).HasForeignKey(h => h.Horario_HorarioId).OnDelete(DeleteBehavior.NoAction);
            //CLAVE FORÁNEA SESION-SALA
            modelBuilder.Entity<Sesion>().HasOne<Sala>(sa => sa.Sala).WithMany(se => se.Sesiones).HasForeignKey(sa => sa.Sala_SalaId).OnDelete(DeleteBehavior.NoAction);
            //CLAVE FORÁNEA SESION-PELICULA
            modelBuilder.Entity<Sesion>().HasOne<Pelicula>(p => p.Pelicula).WithMany(se => se.Sesiones).HasForeignKey(p => p.Pelicula_PeliculaId).OnDelete(DeleteBehavior.NoAction);

            //CLAVE FORÁNEA SESION_ENTRADA_BUTACA-BUTACA
            modelBuilder.Entity<SesionEntradaButaca>().HasOne<Butaca>(b => b.Butaca).WithMany(en => en.Entradas).HasForeignKey(b => b.Butaca_ButacaId).OnDelete(DeleteBehavior.NoAction);

            //CLAVE FORÁNEA SESION_ENTRADA_BUTACA-SESION
            modelBuilder.Entity<SesionEntradaButaca>().HasOne<Sesion>(se => se.Sesion).WithMany(en => en.Entradas).HasForeignKey(se => se.Sesion_SesionId).OnDelete(DeleteBehavior.NoAction);

            //CLAVE ALTERNATIVA EMPLEADA DNI
            modelBuilder.Entity<Empleada>().HasAlternateKey(e => e.DNI);

            //CLAVE PRIMARIA SESION_ENTRADA_BUTACA
            modelBuilder.Entity<SesionEntradaButaca>().HasKey(seb => new { seb.Sesion_SesionId, seb.Butaca_ButacaId });

            //DATOS EN ROL
            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    RolId = 1,
                    NombreRol = "Taquillera"
                },
                new Rol
                {
                    RolId = 2,
                    NombreRol = "Acomodadora"
                }
                ,
                new Rol

                {
                    RolId = 3,
                    NombreRol = "Encargada"
                }
                ,
                new Rol
                {
                    RolId = 4,
                    NombreRol = "Dependienta"
                }
            );

            //DATOS EN EMPLEADA
            modelBuilder.Entity<Empleada>().HasData(
                new Empleada
                {
                    EmpleadaId = 1,
                    Nombre = "Noemí",
                    Apellidos = "Álvarez Bouzán",
                    DNI = "44458565W",
                    Correo = "noemi@gmail.com",
                    Clave = "abc123.",
                    Rol_RolId = 3,
                    Cine_CineId = 1
                },
                new Empleada
                {
                    EmpleadaId = 2,
                    Nombre = "María",
                    Apellidos = "Pérez Sánchez",
                    DNI = "11111111A",
                    Correo = "maria@gmail.com",
                    Clave = "abc123.",
                    Rol_RolId = 3,
                    Cine_CineId = 2
                }
            );

            //DATOS EN CINES
            modelBuilder.Entity<Cine>().HasData(
                new Cine
                {
                    CineId = 1,
                    Nombre = "AstroCines",
                    Nsalas = 8,
                    Nespectadores = 800
                },
                new Cine
                {
                    CineId = 2,
                    Nombre = "TeleCines",
                    Nsalas = 5,
                    Nespectadores = 500
                }

            );

            //DATOS EN CARTELERA
            modelBuilder.Entity<Pelicula>().HasData(
                new Pelicula
                {
                    PeliculaId = 1,
                    Nombre = "FAST & FURIOUS X",
                    Path = "C:/Users/Noemi/Desktop/PROYECTO_FINAL/CINEMA_ORM_InstaladorAdvanced/CINEMA_ORM/CINEMA_ORM/Imagenes/fast-furious-x-64479774b96df.jpg",
                    Imagen = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/fast-furious-x-64479774b96df.jpg")),
                    Recomendacion = "No recomendada para menores de 12 años",
                    Lanzamiento = "Viernes, 19 de mayo de 2023",
                    Genero = "Acción",
                    Reparto = "Vin Diesel, Jason Momoa, Michelle Rodriguez, Nathalie Emmanuel",
                    Director = "Louis Leterrier",
                    Nacionalidad = "EEUU",
                    Distribuidora = "UNIVERSAL",
                    Sinopsis = "Dom Toretto y su familia no pueden llevar una vida tranquila como desearían y tienen que volver a formar un " +
                    "equipo para sobrevivir en el mundo cruel que les ha tocado. Ahora son el objetivo del hijo del narcotraficante Hernán Reyes, " +
                    "quién clama venganza. Fast & furious X será el último filme de la saga callejera por excelencia. " +
                    "Vin Diesel (El último cazador de brujas) será el protagonista del final de la franquicia, " +
                    "que nos ofrecerá más carreras, velocidad y acción en su desenlace."
                },
                new Pelicula
                {
                    PeliculaId = 2,
                    Nombre = "GUARDIANES DE LA GALAXIA: VOLUMEN 3",
                    Path = "C:/Users/Noemi/Desktop/PROYECTO_FINAL/CINEMA_ORM_InstaladorAdvanced/CINEMA_ORM/CINEMA_ORM/Imagenes/guardianes-de-la-galaxia-volumen-3-643d243c846f8.jpg",
                    Imagen = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/guardianes-de-la-galaxia-volumen-3-643d243c846f8.jpg")),
                    Recomendacion = "No recomendada para menores de 12 años",
                    Lanzamiento = "Jueves, 04 de mayo de 2023",
                    Genero = "Acción",
                    Reparto = "Chris Pratt, Zoe Saldana, Dave Bautista, Elizabeth Debicki, Pom Klementieff",
                    Director = "James Gunn",
                    Nacionalidad = "EEUU",
                    Distribuidora = "DISNEY",
                    Sinopsis = "Sigue a Star-Lord, todavía recuperándose de la pérdida de Gamora, que debe reunir a su equipo para defender el universo " +
                    "junto con la protección de uno de los suyos. Una misión que, si no se completa, podría llevar al final de los Guardianes tal como los conocemos."
                },
                new Pelicula
                {
                    PeliculaId = 3,
                    Nombre = "INDIANA JONES Y EL DIAL DEL DESTINO",
                    Path = "C:/Users/Noemi/Desktop/PROYECTO_FINAL/CINEMA_ORM_InstaladorAdvanced/CINEMA_ORM/CINEMA_ORM/Imagenes/indiana-jones-y-el-dial-del-destino-648989f862964.jpg",
                    Imagen = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/indiana-jones-y-el-dial-del-destino-648989f862964.jpg")),
                    Recomendacion = "Pendiente de clasificación",
                    Lanzamiento = "Miércoles, 28 de junio de 2023",
                    Genero = "Acción",
                    Reparto = "Harrison Ford, Phoebe Waller-Bridge, Mads Mikkelsen",
                    Director = "James Mangold",
                    Nacionalidad = "EEUU",
                    Distribuidora = "DISNEY",
                    Sinopsis = "El arqueólogo Indiana Jones deberá emprender otra aventura contra el tiempo para intentar recuperar un dial legendario que puede cambiar el curso de la historia." +
                    " Acompañado por su ahijada, Jones pronto se encuentra enfrentándose a Jürgen Voller, un ex nazi que trabaja para la NASA. " 
                },
                new Pelicula
                {
                    PeliculaId = 4,
                    Nombre = "LA SIRENITA",
                    Path = "C:/Users/Noemi/Desktop/PROYECTO_FINAL/CINEMA_ORM_InstaladorAdvanced/CINEMA_ORM/CINEMA_ORM/Imagenes/la-sirenita-646c8243e8436.jpg",
                    Imagen = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/la-sirenita-646c8243e8436.jpg")),
                    Recomendacion = "Especialmente rec. para la infancia",
                    Lanzamiento = "Viernes, 26 de mayo de 2023",
                    Genero = "CONCIERTO",
                    Reparto = "Halle Bailey, Daveed Diggs, Javier Bardem, Jonah Hauer-King, Melissa McCarthy",
                    Director = "Rob Marshall",
                    Nacionalidad = "EEUU",
                    Distribuidora = "DISNEY",
                    Sinopsis = "Ariel, la más joven de las hijas del Rey Tritón y la más desafiante, desea saber más sobre el mundo más allá del mar y," +
                    " mientras visita la superficie, se enamora del apuesto Príncipe Eric. Si bien las sirenas tienen prohibido interactuar con los humanos, " +
                    "Ariel debe seguir su corazón. Así, hace un trato con la malvada bruja del mar, Úrsula, que le da la oportunidad de experimentar la vida en la tierra, " +
                    "lo que pone en peligro su vida y la corona de su padre."

                },
               new Pelicula
               {
                    PeliculaId = 5,
                    Nombre = "FLASH",
                    Path = "C:/Users/Noemi/Desktop/PROYECTO_FINAL/CINEMA_ORM_InstaladorAdvanced/CINEMA_ORM/CINEMA_ORM/Imagenes/flash-6489865cbf78d.jpg",
                    Imagen = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/flash-6489865cbf78d.jpg")),
                    Recomendacion = "No recomendada para menores de 12 años",
                    Lanzamiento = "Viernes, 16 de junio de 2023",
                    Genero = "Ciencia ficción",
                    Reparto = "Ezra Miller, Ben Affleck, Michael Keaton, Sasha Calle, Michael Shannon",
                    Director = "Andy Muschietti",
                    Nacionalidad = "EEUU",
                    Distribuidora = "WARNER",
                    Sinopsis = "Los mundos chocan en Flash cuando Barry utiliza sus superpoderes para viajar en el tiempo y cambiar los acontecimientos del pasado." +
                    " Barry intenta salvar a su familia, pero sin saberlo altera el futuro y queda atrapado en una realidad en la que el general Zod ha regresado y" +
                    " amenaza con la aniquilación, pero en la que no hay Superhéroes a los que recurrir. A menos que Barry pueda persuadir a un Batman muy diferente" +
                    " para que salga de su retiro y rescate a un kryptoniano encarcelado... aunque no sea el que está buscando. En última instancia, para salvar el " +
                    "mundo en el que se encuentra y regresar al futuro que conoce, la única esperanza de Barry es luchar por seguir vivo. Pero ¿este último sacrificio " +
                    "será suficiente para reiniciar el universo?"

               },
                new Pelicula
                {
                    PeliculaId = 6,
                    Nombre = "SPIDER-MAN: CRUZANDO EL MULTIVERSO",
                    Path = "C:/Users/Noemi/Desktop/PROYECTO_FINAL/CINEMA_ORM_InstaladorAdvanced/CINEMA_ORM/CINEMA_ORM/Imagenes/spider-man-cruzando-el-multiverso-647708a8e5f5a.jpg",
                    Imagen = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/spider-man-cruzando-el-multiverso-647708a8e5f5a.jpg")),
                    Recomendacion = "No recomendada para menores de 7 años",
                    Lanzamiento = "Viernes, 02 de junio de 2023",
                    Genero = "Animación",
                    Reparto = "Shameik Moore, Hailee Steinfeld, Daniel Kaluuya, Jake Johnson, Issa Rae",
                    Director = "Joaquim Dos Santos, Kemp Powers, Justin Thompson",
                    Nacionalidad = "EEUU",
                    Distribuidora = "SONY",
                    Sinopsis = "Segunda entrega de las aventuras de Miles Morales tras el filme de animación ganador del Oscar Spider-Man: Un nuevo universo (2018). " +
                    "Para demostrar lo que vale y transportase a través del Multiverso, el joven Spider-Man de Brooklyn se aliará con Gwen Stacy. Tras este reencuentro, " +
                    "los dos jóvenes arácnidos conocerán a todo un grupo de élite con los mejores Spider-Man de los diferentes Multiversos. Claro que ser un Hombre Araña " +
                    "también tiene sus sacrificios, y Miles Morales deberá tomar importantes decisiones. Cuando esté en juego salvar cada uno de los mundos, además de a las " +
                    "personas más queridas, Miles optará por hacerlo a su manera."
                },
                new Pelicula
                {
                    PeliculaId = 7,
                    Nombre = "SUPER MARIO BROS. LA PELÍCULA",
                    Path = "C:/Users/Noemi/Desktop/PROYECTO_FINAL/CINEMA_ORM_InstaladorAdvanced/CINEMA_ORM/CINEMA_ORM/Imagenes/JPG000l8.jpg",
                    Imagen = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/JPG000l8.jpg")),
                    Recomendacion = "Para todos los públicos",
                    Lanzamiento = "Miércoles, 05 de abril de 2023",
                    Genero = "Animación",
                    Reparto = "Chris Pratt, Anya Taylor-Joy, Seth Rogen",
                    Director = "Aaron Horvath, Michael Jelenic",
                    Nacionalidad = "EEUU",
                    Distribuidora = "UNIVERSAL",
                    Sinopsis = "Película de animación basada en la franquicia de videojuegos Super Mario Bros. de Nintendo." +
                    " Está producida por Shigeru Miyamoto, creador de la licencia, y la producirá y animará Illumination Entertainment (Los Minion)."
                },
                new Pelicula
                {
                    PeliculaId = 8,
                    Nombre = "TRANSFORMERS: EL DESPERTAR DE LAS BESTIAS",
                    Path = "C:/Users/Noemi/Desktop/PROYECTO_FINAL/CINEMA_ORM_InstaladorAdvanced/CINEMA_ORM/CINEMA_ORM/Imagenes/transformers-el-despertar-de-las-bestias-648189a61942c.jpg",
                    Imagen = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/transformers-el-despertar-de-las-bestias-648189a61942c.jpg")),
                    Recomendacion = "No recomendada para menores de 12 años",
                    Lanzamiento = "Viernes, 09 de junio de 2023",
                    Genero = "Acción",
                    Reparto = "Anthony Ramos, Dominique Fishback, Luna Lauren Velez",
                    Director = "Steven Caple Jr.",
                    Nacionalidad = "EEUU",
                    Distribuidora = "PARAMOUNT",
                    Sinopsis = "Secuela de Transformers: El último caballero (2017) y sexta entrega " +
                    "de la saga Transformers que gira en torno a unos robots extraterrestres conocidos como Autobots," +
                    " que se ocultan en la Tierra transformándose en coches y otros automóviles, para enfrentarse a los malvados Decepticons." +
                    " La película se desarrolla en la ciudad de Nueva York en el año 1994, " +
                    "lugar en el que está habiendo una cruenta batalla entre el equipo formado por los Predicons," +
                    " los Maximals y los Terrorcons cintra los Autobots y los Decepticons. Además de esto los Autobots" +
                    " tendrán que enfrentarse a nuevos enemigos como un nuevo tipo de Transformers muertos vivientes." +
                    " Entre este caos y peligro, Optimus Prime se convertirá en el famoso líder de los Autobots."

                }

            );

            //DATOS EN HORARIO
            modelBuilder.Entity<Horario>().HasData(
                new Horario
                {
                    HorarioId = 1,
                    Hora = "17:00"
                },
                new Horario
                {
                    HorarioId = 2,
                    Hora = "18:30"
                }
                ,
                new Horario

                {
                    HorarioId = 3,
                    Hora = "19:00"
                }
                ,
                new Horario
                {
                    HorarioId = 4,
                    Hora = "20:00"
                }
                ,
                new Horario
                {
                    HorarioId = 5,
                    Hora = "20:30"
                }
                ,
                new Horario
                {
                    HorarioId = 6,
                    Hora = "21:00"
                }
                ,
                new Horario
                {
                    HorarioId = 7,
                    Hora = "22:00"
                }
            );
        }
    }
}
