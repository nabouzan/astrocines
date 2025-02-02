using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;

namespace CINEMA_ORM.Modelo
{
    public class Pelicula
    {
        public Pelicula()
        {
            Sesiones = new HashSet<Sesion>();
            //this.Images = new List<string>();
        }

        public int  PeliculaId { get; set; }
        [Required(ErrorMessage = "Se requiere el campo de Nombre")]
        [StringLength(maximumLength: 70, ErrorMessage = "Longitud máxima admitida 70 car")]
        public string Nombre { get; set; }
        //public Image Imagen { get; set; }
        public byte[]  Imagen { get; set; }
        //public List<String>  Images { get; set; }
        public string Path { get; set; }

        [Required(ErrorMessage = "Se requiere el campo Recomendación")]
        [StringLength(maximumLength: 100, ErrorMessage = "Longitud máxima admitida 100 car")]
        public string Recomendacion { get; set; }

        [Required(ErrorMessage = "Se requiere el campo de Lanzamiento")]
        [StringLength(maximumLength: 70, ErrorMessage = "Longitud máxima admitida 70 car")]
        public string Lanzamiento { get; set; }

        [Required(ErrorMessage = "Se requiere el campo de Género")]
        [StringLength(maximumLength: 70, ErrorMessage = "Longitud máxima admitida 100 car")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Se requiere el campo Reparto")]
        [StringLength(maximumLength: 250, ErrorMessage = "Longitud máxima admitida 250 car")]
        public string Reparto { get; set; }

        [Required(ErrorMessage = "Se requiere el campo Director/a")]
        [StringLength(maximumLength: 170, ErrorMessage = "Longitud máxima admitida 170 car")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Se requiere el campo de Nacionalidad")]
        [StringLength(maximumLength: 70, ErrorMessage = "Longitud máxima admitida 70 car")]
        public string Nacionalidad { get; set; }

        [Required(ErrorMessage = "Se requiere el campo Distribuidora")]
        [StringLength(maximumLength: 70, ErrorMessage = "Longitud máxima admitida 70 car")]
        public string Distribuidora { get; set; }

        public string Sinopsis { get; set; }

        //COLECCIÓN DE SESIONES
        public virtual ICollection<Sesion> Sesiones { get; set; }

        //public BitmapImage img
        //{
        //    get
        //    {
        //        Image img = new Image();
        //        img.Source = new BitmapImage(new Uri(Path));
        //        img.Stretch = Stretch.UniformToFill;
        //        return new BitmapImage(new Uri(Path));
        //    }
        //}

    }
}
