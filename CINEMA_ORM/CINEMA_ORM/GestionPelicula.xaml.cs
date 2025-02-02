using CINEMA_ORM.Modelo;
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
using System.Windows.Shapes;

namespace CINEMA_ORM
{
    /// <summary>
    /// Lógica de interacción para GestionPelicula.xaml
    /// </summary>
    public partial class GestionPelicula : Window
    {
        Pelicula pelicula;

        public GestionPelicula(Pelicula peli)
        {
            InitializeComponent();
            GB_FORMULARIO.DataContext = peli;
            pelicula = peli;
            IM_CARTEL.Source = ToImage(pelicula.Imagen);
        }

        // De la base de Datos para asignar a un control Image
        private BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        // Del control al campo de la base de datos
        public byte[] bitmapImageToBytes(BitmapImage image)
        {
            if (image == null) { return null; }
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                //Ruta específica del archivo
                filePath = openFileDialog.FileName;
                TB_RUTA.Text = filePath; 
                pelicula.Path = filePath;   

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.DecodePixelHeight = 400;
                bitmap.EndInit();

                if (IM_CARTEL.Source != null || pelicula.Imagen != null)
                {
                    IM_CARTEL.Source = bitmap;
                    pelicula.Imagen = bitmapImageToBytes(bitmap);
                }

            }
        }

        //Método que modifica una película
        private void BT_MODIFICAR_Click(object sender, RoutedEventArgs e)
        {
            string errores = Validacion.errores(pelicula);

            if (errores.Equals(""))
            {
                VentanaPrincipal.bd.PeliculaRepository.Update(pelicula);
                VentanaPrincipal.bd.Save();
                MessageBox.Show("Película modificada correctamente",
                    "Gestión de película", MessageBoxButton.OK, MessageBoxImage.Information);
       
            }
            else MessageBox.Show(errores);
        }
        //Método que que cancela la operación y cierra la ventana
        private void BT_CANCELAR_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
            MessageBox.Show("¿Quieres salir de gestión de película?",
            "Gestión película", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
            else if (result == MessageBoxResult.No)
            {

            }
        }

    }
}
