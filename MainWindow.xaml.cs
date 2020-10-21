using RegistroPedidos.UI.Registro;
using System;
using System.Collections.Generic;
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

namespace RegistroPedidos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void rOrdenesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new rOrdenes().Show();
        }

        public void cOrdenesMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        public void cProductosMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        public void cSuplidoresMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
