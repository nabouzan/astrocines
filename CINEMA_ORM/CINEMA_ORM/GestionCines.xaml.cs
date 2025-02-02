using CINEMA_ORM.DAL;
using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Paragraph = iTextSharp.text.Paragraph;
using MaterialDesignColors;
using Org.BouncyCastle.Asn1.Cms;
using System.Collections;
using System.Data;
using ControlzEx.Standard;

namespace CINEMA_ORM
{
    /// <summary>
    /// Lógica de interacción para GestionCines.xaml
    /// </summary>
    public partial class GestionCines : Page
    {
        //UnidadDeTrabajo bd = new UnidadDeTrabajo();
        Cine cine = new Cine();
        Boolean nuevo = true;
        Boolean abierto = true;
        public GestionCines()
        {
            InitializeComponent();
            //Guardamos el cine en el formulario
            GB_FORMULARIO.DataContext = cine;
            //Clase que cambia la cultura a la española
            //CultureInfo cultureInfo = new CultureInfo("es-SP");
            //Método que devuelve una instancia de XmlLanguage en función de una cadena que representa el idioma de RFC 3066.
            //DP_FECHA_REGISTRO.Language = System.Windows.Markup.XmlLanguage.GetLanguage(cultureInfo.Name);
            //Fecha actual asignada al calendario
            //cine.FechaRegistro = DateTime.Now;
            //Mostramos en el datagrid todos los cines de la BD
            DG_CINES.ItemsSource = VentanaPrincipal.bd.CineRepository.GetAll();
        }
        private void DgCines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_CINES.SelectedIndex != -1)
            {
                cine = (Cine)DG_CINES.SelectedItem;
                nuevo = false;
                //abierto = true;
                GB_FORMULARIO.DataContext = cine;
            }
        }
        private void Limpiar()
        {
            cine = new Cine();
            //Fecha actual asignada al calendario
            //cine.FechaRegistro = DateTime.Now;
            GB_FORMULARIO.DataContext = cine;
            nuevo = true;
            //abierto = false;
        }
        private void BT_REGISTRAR_Click(object sender, RoutedEventArgs e)
        {
            string errores = Validacion.errores(cine);

            if (errores.Equals(""))
            {
                if (nuevo)//Añadir
                {
                    VentanaPrincipal.bd.CineRepository.Añadir(cine);
                    VentanaPrincipal.bd.Save();
                    MessageBox.Show("Cine insertado correctamente",
                    "Gestión de cines", MessageBoxButton.OK, MessageBoxImage.Information);
                    DG_CINES.ItemsSource = VentanaPrincipal.bd.CineRepository.GetAll();
                }
                else//Modificar
                {
                    VentanaPrincipal.bd.CineRepository.Update(cine);
                    VentanaPrincipal.bd.Save();
                    MessageBox.Show("Cine modificado correctamente",
                    "Gestión de cines", MessageBoxButton.OK, MessageBoxImage.Information);
                    DG_CINES.ItemsSource = VentanaPrincipal.bd.CineRepository.GetAll();
                }
                Limpiar();
            }
            else MessageBox.Show(errores);
        }

        private void BT_CANCELAR_Click(object sender, RoutedEventArgs e)
        {
            cine = new Cine();
            //CultureInfo cultureInfo = new CultureInfo("es-SP");
            //DP_FECHA_REGISTRO.Language = System.Windows.Markup.XmlLanguage.GetLanguage(cultureInfo.Name);
            //cine.FechaRegistro = DateTime.Now;
            GB_FORMULARIO.DataContext = cine;
        }
        private void TB_SALAS_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TB_SALAS.Text, "[^0-9]"))
            {
                MessageBox.Show("Por favor introduce solamente números.",
                    "Gestión de cines", MessageBoxButton.OK, MessageBoxImage.Error);
                TB_SALAS.Text = TB_SALAS.Text.Remove(TB_SALAS.Text.Length - 1);
            }
        }
        private void TB_ESPECTADORES_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(TB_ESPECTADORES.Text, "[^0-9]"))
            {
                MessageBox.Show("Por favor introduce solamente números.", 
                    "Gestión de cines", MessageBoxButton.OK, MessageBoxImage.Error);
                TB_ESPECTADORES.Text = TB_ESPECTADORES.Text.Remove(TB_ESPECTADORES.Text.Length - 1);
            }
        }
        //Método que elimina un cine con todo lo que abarca si confirmas
        private void BT_ELIMINAR_Click(object sender, RoutedEventArgs e)
        {
            if (DG_CINES.SelectedItem != null)
            {
                MessageBoxResult result = 
                    MessageBox.Show("¡Cuidado! ¿Quieres borrar las empleadas de este cine?",
                    "Gestión de cines", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                if (result == MessageBoxResult.Yes) 
                {
                    VentanaPrincipal.bd.CineRepository.Delete(cine);
                    VentanaPrincipal.bd.Save();
                    MessageBox.Show("Cine eliminado correctamente");
                    DG_CINES.ItemsSource = VentanaPrincipal.bd.CineRepository.GetAll();
                    Limpiar();
                }else if(result == MessageBoxResult.No) 
                {
                    DG_CINES.ItemsSource = VentanaPrincipal.bd.CineRepository.GetAll();
                }
            }
        }

        //Método que genera informe al hacer click en el botón generar
        private void BT_GENERAR_INFORME_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            if (doc != null)
            {
                if (abierto)
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("InformeCines.pdf",
                    FileMode.Create));
                    abierto = false;
                    //Agregamos metadatos
                    doc.AddAuthor("Noemí Álvarez Bouzán");
                    doc.AddCreator("Creación de PDF usando iTextSharp");
                    doc.AddKeywords("PDF tutorial");
                    doc.AddTitle("Informe Cines - Creación de PDF usando iTextSharp");
                    // Abrimos el documento
                    doc.Open();
                    // Agregamos un párrafo al documento
                    doc.Add(new Paragraph("Creación de PDF usando iTextSharp    " +
                        "   " +
                        "   "));
                    doc.Add(new Paragraph(""));


                    var tab = new PdfPTable(3) { WidthPercentage = 100 };
                    // Esta es la primera fila
                    tab.AddCell("Nombre");
                    tab.AddCell("Nsalas");
                    tab.AddCell("Nespectadores");

                    // Segunda fila
                    tab.AddCell(TB_NOMBRE.Text);
                    tab.AddCell(TB_SALAS.Text);
                    tab.AddCell(TB_ESPECTADORES.Text);

                    doc.Add(tab);
                    // Cerramos documento
                    doc.Close();
                    //Cerramos la instacia del escritor de pdf
                    writer.Close();
                    //Mostramos el informe con el visualizador
                    new Wpdf(new Uri(Environment.CurrentDirectory + "\\InformeCines.pdf")).Show();
                }
               
            }
        }
       
    }
}
