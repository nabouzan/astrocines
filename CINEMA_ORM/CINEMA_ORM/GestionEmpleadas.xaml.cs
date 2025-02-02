using CINEMA_ORM.DAL;
using CINEMA_ORM.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Paragraph = iTextSharp.text.Paragraph;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection.PortableExecutable;

namespace CINEMA_ORM
{
    /// <summary>
    /// Lógica de interacción para GestionEmpleadas.xaml
    /// </summary>
    public partial class GestionEmpleadas : Page
    {
        //UnidadDeTrabajo bd = new UnidadDeTrabajo();
        Boolean nuevo = true;
        Empleada empleadx = new Empleada();
        Rol rol = new Rol();
        Cine cine = new Cine();
        Boolean abierto = true;
        public GestionEmpleadas()
        {
            InitializeComponent();
            //Mostramos en el datagrid tod@s l@s emplead@s de la BD 
            DG_EMPLEADX.ItemsSource = VentanaPrincipal.bd.EmpleadaRepository.GetAll();
            //GroupBox formulario emplead@
            GB_FORMULARIO.DataContext = empleadx;
          
            //Combo Roles
            CB_ROL_ID.ItemsSource = VentanaPrincipal.bd.RolRepository.GetAll();
            CB_ROL_ID.DisplayMemberPath = "NombreRol";
            CB_ROL_ID.SelectedValuePath = "RolId";

            //Combo Cines
            CB_CINE_ID.ItemsSource = VentanaPrincipal.bd.CineRepository.GetAll();
            CB_CINE_ID.DisplayMemberPath = "Nombre";
            CB_CINE_ID.SelectedValuePath = "CineId";

        }
        //Método al seleccionar una fila del datagrid
        private void DG_EMPLEADX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DG_EMPLEADX.SelectedIndex != -1)
            {

                empleadx = (Empleada)DG_EMPLEADX.SelectedItem;
                PB_PASS.Password = empleadx.Clave;
                PB_PASS_REP.Password = empleadx.Clave;
                nuevo = false;
                GB_FORMULARIO.DataContext = empleadx;
            }
        }

        private void CB_ROL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_ROL_ID.SelectedIndex != -1)
            {
                //rol = (Rol)CB_ROL_ID.SelectedItem;
                //empleadx.Rol_RolId = rol.RolId;
                //GD_ROL.DataContext = rol;
                rol = (Rol)CB_ROL_ID.SelectedItem;
                //empleadx.Rol= rol;
                //GD_ROL.DataContext = rol;
                rol = VentanaPrincipal.bd.RolRepository.RolCompleto(rol.RolId);
            }

        }
        private void CB_CINE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_CINE_ID.SelectedIndex != -1)
            {
                cine = (Cine)CB_CINE_ID.SelectedItem;
                //GD_CINE.DataContext = cine;
                //Cine cine = (Cine)CB_CINE_ID.SelectedItem;
                //empleadx.Cine = cine;   
                cine = VentanaPrincipal.bd.CineRepository.CineCompleto(cine.CineId);
            }

        }

        private void BT_LIMPIAR_Click(object sender, RoutedEventArgs e)
        {
            empleadx = new Empleada();
            PB_PASS.Password = "";
            PB_PASS_REP.Password = "";
            GB_FORMULARIO.DataContext = empleadx;
            nuevo = true;
        }

        private void BT_ELIMINAR_Click(object sender, RoutedEventArgs e)
        {
            if (DG_EMPLEADX.SelectedItem != null)
            {
                VentanaPrincipal.bd.EmpleadaRepository.Delete(empleadx);
                VentanaPrincipal.bd.Save();
                DG_EMPLEADX.ItemsSource = VentanaPrincipal.bd.EmpleadaRepository.GetAll();
            }
        }
        //Método que guarda empleada dependiendo de la booleana
        private void BT_GUARDAR_Click(object sender, RoutedEventArgs e)
        {
            string errores = Validacion.errores(empleadx);

            if (errores.Equals(""))
            {
                if (nuevo)// Añadir
                { 
                    //Si los dos campos coinciden engadimos
                    if (PB_PASS.Password.ToString() == PB_PASS_REP.Password.ToString())
                    {
                        empleadx.Clave = PB_PASS.Password;
                        VentanaPrincipal.bd.EmpleadaRepository.Añadir(empleadx);
                        VentanaPrincipal.bd.Save();
                        MessageBox.Show("Registrad@ correctamente",
                        "Gestión de empleadas", MessageBoxButton.OK, MessageBoxImage.Information);
                        DG_EMPLEADX.ItemsSource = VentanaPrincipal.bd.EmpleadaRepository.GetAll();
                    }
                    // Si no mensaje de error
                    else
                    {
                        MessageBox.Show("La contraseña debe ser la misma",
                        "Gestión de empleadas", MessageBoxButton.OK, MessageBoxImage.Error);
                        PB_PASS_REP.Focus();
                    }

                }
                else // Modificar
                {
                    //Si los dos campos coinciden modificamos
                    if (PB_PASS.Password.ToString() == PB_PASS_REP.Password.ToString())
                    {
                        empleadx.Clave = PB_PASS.Password;
                        VentanaPrincipal.bd.EmpleadaRepository.Update(empleadx);
                        VentanaPrincipal.bd.Save();
                        MessageBox.Show("Modificad@ correctamente",
                        "Gestión de empleadas", MessageBoxButton.OK, MessageBoxImage.Information);
                        DG_EMPLEADX.ItemsSource = VentanaPrincipal.bd.EmpleadaRepository.GetAll();
                    }
                    // Si no mensaje de error
                    else
                    {
                        MessageBox.Show("La contraseña debe ser la misma",
                        "Gestión de empleadas", MessageBoxButton.OK, MessageBoxImage.Error);
                        PB_PASS_REP.Focus();
                    }
                }
                //BT_LIMPIAR_Click(sender, e);
            }
            else MessageBox.Show(errores);
        }
        //Método que genera un pdf de 0 y lo visualizamos en un webbrowser
        private void BT_GENERAR_INFORME_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            if (doc != null)
            {
                if (abierto)
                {
                    // Asignamos el nombre de archivo InformeEmpleadas.pdf
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("InformeEmpleadas.pdf",
                        FileMode.Create));
                    abierto = false;
                    doc.Open();
                    Paragraph title = new Paragraph();
                    title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f, BaseColor.BLUE);
                    title.Add("Informe Empleada");
                    doc.Add(title);
                    // Agregamos un parrafo vacío como separación.
                    doc.Add(new Paragraph(" "));

                    PdfPTable table = new PdfPTable(9) { WidthPercentage = 100 };
                    // Esta es la primera fila
                    table.AddCell("Nombre");
                    table.AddCell("Apellidos");
                    table.AddCell("DNI");
                    table.AddCell("Domicilio");
                    table.AddCell("Teléfono");
                    table.AddCell("Email");
                    table.AddCell("CineId");
                    table.AddCell("RolId");
                    table.AddCell("Clave");

                    //Segunda fila
                    if (TB_NOMBRE.Text != null)
                    {
                        table.AddCell(TB_NOMBRE.Text);
                    }
                    if (TB_APELLIDOS.Text != null)
                    {
                        table.AddCell(TB_APELLIDOS.Text);
                    }
                    if (TB_DNI.Text != null)
                    {
                        table.AddCell(TB_DNI.Text);
                    }
                    if (TB_DIRECCION.Text != null)
                    {
                        table.AddCell(TB_DIRECCION.Text);
                    }
                    if (TB_TELEFONO.Text != null)
                    {
                        table.AddCell(TB_TELEFONO.Text);
                    }
                    if (TB_EMAIL.Text != null)
                    {
                        table.AddCell(TB_EMAIL.Text);
                    }
                    if (CB_CINE_ID.Text != null)
                    {
                        table.AddCell(CB_CINE_ID.Text);
                    }
                    if (CB_ROL_ID.Text != null)
                    {
                        table.AddCell(CB_ROL_ID.Text);
                    }
                    if (PB_PASS.Password != null)
                    {
                        table.AddCell(PB_PASS.Password);
                    }
                    // Agregamos la tabla al documento
                    doc.Add(table);
                    // Cerramos documento
                    doc.Close();
                    //Cerramos la instacia del escritor de pdf
                    writer.Close();
                    //Visualizamos el informe
                    new Wpdf(new Uri(Environment.CurrentDirectory + "\\InformeEmpleadas.pdf")).Show();
                }
            }
        }
    }
}
