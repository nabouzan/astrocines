using CINEMA_ORM.DAL;
using CINEMA_ORM.Modelo;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UnidadDeTrabajo bd = new UnidadDeTrabajo();
        Empleada empleadx ;
        public MainWindow()
        {
            InitializeComponent();
            //if (empleadx!=null)
            //{
            //empleadx = bd.EmpleadaRepository.EmpleadaCompleta(TB_USER.Text);
            //Traemos la primera empleada de la base de datos
            //empleadx = bd.EmpleadaRepository.GetAll()[0];
            //Los mostramos en los dos campos del formulario
           // TB_USER.Text = empleadx.Correo;
           // PB_PASS.Password = empleadx.Clave;
            //}
        }

        private void BT_ENTRAR_MouseDown(object sender, RoutedEventArgs e)
        {
            //Validamos que los campos no estén vacíos
            if (TB_USER.Text.Length == 0)
            {
                TB_ERROR.Text = "Introduce un/a usuari@. Por favor.";
                TB_USER.Focus();

            }
            else if (TB_USER.Text.Length < 2)
            {
                TB_ERROR.Text = "Introduce un/a usuari@ válid@.";
                TB_USER.Select(0, TB_USER.Text.Length);
                TB_USER.Focus();

            }
            else if (PB_PASS.Password.Length == 0)
            {
                TB_ERROR.Text = "Introduce una contraseña. Por favor.";
                PB_PASS.Focus();

            }
            else if (PB_PASS.Password.Length < 5)
            {
                TB_ERROR.Text = "Por favor introduce un password válido.";
                PB_PASS.Focus();
            }
            //Entramos en la ventana principal de la app si la empleada es distinta de null
            else 
            {
               empleadx = bd.EmpleadaRepository.EmpleadaCompleta(TB_USER.Text, PB_PASS.Password.ToString());
               if(empleadx != null)
                {
                    VentanaPrincipal principal = new VentanaPrincipal(empleadx);
                    principal.Show();
                    this.Close();
                }
               TB_ERROR.Text = "Por favor debes introducir un correo y pass válidos";
            }
        }

        //private void BT_REGISTRAR_MouseDown(object sender, RoutedEventArgs e)
        //{
        //    Registrar registrar = new Registrar();
        //    registrar.Show();
        //    this.Close();

        //}
        private void BT_SALIR_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
            MessageBox.Show("¡Cuidado! ¿Quieres salir de la aplicación?",
                    "Log in", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                if (result == MessageBoxResult.Yes)
                {
                    Close();
                }
                else if (result == MessageBoxResult.No)
                {
                    
                }
        }
        private void BT_AYUDA_Click(object sender, RoutedEventArgs e)
        {

            new Wpdf(new Uri(Environment.CurrentDirectory + "\\Documentos\\ManualUsuaria.pdf")).Show();
        }
    }
}
