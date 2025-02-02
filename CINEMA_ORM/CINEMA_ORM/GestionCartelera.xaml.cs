using CINEMA_ORM.Modelo;
using iTextSharp.text.pdf.parser;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CINEMA_ORM
{
    /// <summary>
    /// Lógica de interacción para GestionCartelera.xaml
    /// </summary>
    public partial class GestionCartelera : Page
    {
        Pelicula pelicula = new Pelicula();
        //Boolean nuevo = true;
        //Boolean abierto = true;
        public GestionCartelera()
        {
            InitializeComponent();
            //Mostramos en el datagrid todas las películas de la BD
            DG_PELICULAS.ItemsSource = VentanaPrincipal.bd.PeliculaRepository.GetAll();
      
        }
        private void DgPeliculas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_PELICULAS.SelectedIndex != -1)
            {
                pelicula = (Pelicula)DG_PELICULAS.SelectedItem;
                GestionPelicula gestionPelicula = new GestionPelicula( pelicula );
                gestionPelicula.Show();
            }
        }

        private void BT_REGISTRAR_Click(object sender, RoutedEventArgs e)
        {
            NuevaPelicula nueva = new NuevaPelicula();
            nueva.Show();
            //Mostramos en el datagrid todas las películas de la BD
            DG_PELICULAS.ItemsSource = VentanaPrincipal.bd.PeliculaRepository.GetAll();
        }

        private void BT_CANCELAR_Click(object sender, RoutedEventArgs e)
        {
            pelicula = new Pelicula();
        }
        //Método que elimina una película con todo lo que abarca si confirmas
        private void BT_ELIMINAR_Click(object sender, RoutedEventArgs e)
        {
            if (DG_PELICULAS.SelectedItem != null)
            {
                MessageBoxResult result =
                    MessageBox.Show("¡Cuidado! ¿Quieres borrar la película?",
                    "Gestión de cartelera", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                if (result == MessageBoxResult.Yes)
                {
                    VentanaPrincipal.bd.PeliculaRepository.Delete(pelicula);
                    VentanaPrincipal.bd.Save();
                    MessageBox.Show("Película eliminada correctamente",
                        "Gestión de cartelera", MessageBoxButton.OK, MessageBoxImage.Information);
                    DG_PELICULAS.ItemsSource = VentanaPrincipal.bd.PeliculaRepository.GetAll();
                }
                else if (result == MessageBoxResult.No)
                {
                    DG_PELICULAS.ItemsSource = VentanaPrincipal.bd.PeliculaRepository.GetAll();
                }
            }
        }
    }
}
