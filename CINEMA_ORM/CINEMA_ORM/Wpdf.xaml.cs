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
    /// Lógica de interacción para Wpdf.xaml
    /// </summary>
    public partial class Wpdf : Window
    {
        public Wpdf(Uri uRuta)
        {
            InitializeComponent();
            WB_PDF.Source = uRuta;
        }
    }
}
