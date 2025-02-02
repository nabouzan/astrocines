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
    /// Lógica de interacción para NuevaPelicula.xaml
    /// </summary>
    public partial class NuevaPelicula : Window
    {
        Pelicula pelicula = new Pelicula();
        public NuevaPelicula()
        {
            InitializeComponent();
            GB_FORMULARIO_NEW.DataContext = pelicula;
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
        //Método para cargar el cartel
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

                IM_CARTEL_NEW.Source = bitmap;
                pelicula.Imagen = bitmapImageToBytes(bitmap);
                
            }
        }
        //Método que borra el formulario
        private void Limpiar()
        {
            pelicula = new Pelicula();
            GB_FORMULARIO_NEW.DataContext = pelicula;
            IM_CARTEL_NEW.Source = new BitmapImage(); 
        }

        //Método que añade una nueva película
        private void BT_REGISTRAR_Click(object sender, RoutedEventArgs e)
        {
            string errores = Validacion.errores(pelicula);

            if (errores.Equals(""))
            {
                VentanaPrincipal.bd.PeliculaRepository.Añadir(pelicula);
                VentanaPrincipal.bd.Save();
                MessageBox.Show("Película insertada correctamente",
                "Gestión de película", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else MessageBox.Show(errores);
        }
        //Método del botón borrar que llama a limpiar 
        private void BT_BORRAR_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
