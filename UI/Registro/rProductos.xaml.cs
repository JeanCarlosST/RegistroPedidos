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
    /// Interaction logic for rProductos.xaml
    /// </summary>
    public partial class rProductos : Window
    {
        Productos producto;
        public rProductos()
        {
            InitializeComponent();
            Limpiar();
        }

        public void Limpiar()
        {
            producto = new Productos();
            DataContext = producto;
        }

        public bool Validar()
        {
            DescripcionTextBox.Text = DescripcionTextBox.Text.Trim();
            if (DescripcionTextBox.Text.Length == 0)
            {
                MessageBox.Show("Ingrese una descripción al producto", "Registro de productos",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            CostoTextBox.Text = CostoTextBox.Text.Replace("$", "");
            if(Utilities.ToDecimal(CostoTextBox.Text) == 0)
            {
                MessageBox.Show("Ingrese un costo al producto que sea válido o mayor a cero", "Registro de productos",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void BuscarBoton_Click(object sender, RoutedEventArgs e)
        {
            Productos producto = ProductosBLL.Buscar(Utilities.ToInt(ProductoIdTextBox.Text));

            if (producto != null)
            {
                this.producto = producto;
            }
            else
            {
                MessageBox.Show("No se encontró ningún producto", "Registro de productos",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                this.producto = new Productos();
            }

            this.DataContext = producto;
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosBLL.Eliminar(Utilities.ToInt(ProductoIdTextBox.Text)))
            {
                MessageBox.Show("Producto borrado correctamente", "Registro de productos",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }

        private void GuardarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (ProductosBLL.Guardar(producto))
            {
                MessageBox.Show("Se ha guardado correctamente", "Registro de productos",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("Hubo un error, no se puedo guardar", "Registro de productos",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

    }
}
