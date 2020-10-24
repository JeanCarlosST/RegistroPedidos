using RegistroPedidos.BLL;
using RegistroPedidos.Entidades;
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

namespace RegistroPedidos.UI.Registro
{
    /// <summary>
    /// Interaction logic for rSuplidor.xaml
    /// </summary>
    public partial class rSuplidores : Window
    {
        Suplidores suplidor;
        public rSuplidores()
        {
            InitializeComponent();
            Limpiar();
        }

        public void Limpiar()
        {
            suplidor = new Suplidores();
            DataContext = suplidor;
        }

        public bool Validar()
        {
            NombresTextBox.Text = NombresTextBox.Text.Trim();
            if(NombresTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un nombre al suplidor", "Registro de suplidores",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void BuscarBoton_Click(object sender, RoutedEventArgs e)
        {
            Suplidores suplidor = SuplidoresBLL.Buscar(Utilities.ToInt(SuplidorIdTextBox.Text));

            if(suplidor != null)
            {
                this.suplidor = suplidor;
            }
            else
            {
                MessageBox.Show("No se encontró ningún suplidor", "Registro de suplidores",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                this.suplidor = new Suplidores();
            }

            this.DataContext = suplidor;
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            if(SuplidoresBLL.Eliminar(Utilities.ToInt(SuplidorIdTextBox.Text)))
            {
                MessageBox.Show("Suplidor borrado correctamente", "Registro de suplidores",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }

        private void GuardarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if(SuplidoresBLL.Guardar(suplidor))
            {
                MessageBox.Show("Se ha guardado correctamente", "Registro de suplidores",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("Hubo un error, no se puedo guardar", "Registro de suplidores",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
    }
}
