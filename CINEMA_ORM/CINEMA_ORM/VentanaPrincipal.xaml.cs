using CINEMA_ORM.DAL;
using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CINEMA_ORM
{
    /// <summary>
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public static UnidadDeTrabajo bd = new UnidadDeTrabajo();
        Empleada emp;
        public VentanaPrincipal(Empleada empleada)
        {
            InitializeComponent();
            emp = empleada;
        }

        private void BT_MANTENIMIENTO_CARTELERA_Click(object sender, RoutedEventArgs e)
        {
            FR_MARCO.Navigate(new System.Uri("GestionCartelera.xaml", UriKind.RelativeOrAbsolute));
        }
        private void BT_MANTENIMIENTO_CINES_Click(object sender, RoutedEventArgs e)
        {
            FR_MARCO.Navigate(new System.Uri("GestionCines.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BT_MANTENIMIENTO_EMPLEADX_Click(object sender, RoutedEventArgs e)
        {
            FR_MARCO.Navigate(new System.Uri("GestionEmpleadas.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BT_SALIR_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
            MessageBox.Show("¡Cuidado! ¿Quieres salir de la aplicación?",
            "Ventana principal", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
            else if (result == MessageBoxResult.No)
            {

            }
        }

        private void BT_AYUDA_Click(object sender, RoutedEventArgs e)
        {
            new Wpdf(new Uri(Environment.CurrentDirectory + "\\Documentos\\ManualUsuaria.pdf")).Show();

        }

        //private void BT_LOGIN_Click(object sender, RoutedEventArgs e)
        //{
        //    //FR_MARCO.Navigate(new System.Uri("Login.xaml", UriKind.RelativeOrAbsolute));
        //}
    }
}
