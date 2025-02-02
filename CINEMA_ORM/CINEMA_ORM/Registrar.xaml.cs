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
    /// Lógica de interacción para Registrar.xaml
    /// </summary>
    public partial class Registrar : Window
    {
        Boolean nuevo = true;
        UnidadDeTrabajo bd = new UnidadDeTrabajo();
        Empleada empleadx = new Empleada();
        Cine cine = new Cine();
        public Registrar()
        {
            InitializeComponent();
            GB_CUERPO.DataContext = empleadx;
            CB_CINE_ID.ItemsSource = bd.CineRepository.GetAll();
            CB_CINE_ID.DisplayMemberPath = "Nombre";
            CB_CINE_ID.SelectedValuePath = "CineId";
        }
        private void CB_CINE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_CINE_ID.SelectedIndex != -1)
            {
                cine = (Cine)CB_CINE_ID.SelectedItem;
                //Cine cine = (Cine)CB_CINE_ID.SelectedItem;
                //empleadx.Cine = cine;   
            }

        }

        private void BT_VOLVER_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            this.Close();

        }

        private void BT_LIMPIAR_Click(object sender, RoutedEventArgs e)
        {
            empleadx = new Empleada();
            GB_CUERPO.DataContext = empleadx;
            nuevo = true;
        }

        private void BT_GUARDAR_Click(object sender, RoutedEventArgs e)
        {
            string errores = Validacion.errores(empleadx);

            if (errores.Equals(""))
            {
                if (nuevo)// Añadir
                {
                    if (PB_PASS.Password.ToString() == PB_PASS_REP.Password.ToString() && TB_EMAIL.Text!=null)
                    {          
                        MessageBox.Show("La contraseña es la misma");
                        bd.EmpleadaRepository.Añadir(empleadx);
                        bd.Save();
                        MessageBox.Show("Registrad@ correctamente");
                    }
                    else 
                    {
                        MessageBox.Show("La contraseña debe ser la misma");
                    }
                   
                }
                else // Modificar
                {
                    bd.EmpleadaRepository.Update(empleadx);
                    bd.Save();
                    MessageBox.Show("Modificad@ correctamente");
                   
                }
                BT_LIMPIAR_Click(sender, e);
            }
            else MessageBox.Show(errores);
        }
        private void BT_SALIR_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
