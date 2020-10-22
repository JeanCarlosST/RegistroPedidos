using RegistroPedidos.BLL;
using RegistroPedidos.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
    /// Interaction logic for rOrdenes.xaml
    /// </summary>
    public partial class rOrdenes : Window
    {
        Ordenes orden;
        List<object> detalle;

        public rOrdenes()
        {
            InitializeComponent();
            orden = new Ordenes();
            this.DataContext = orden;

            detalle = new List<object>();

            SuplidorComboBox.ItemsSource = SuplidoresBLL.GetList(s => true);
            SuplidorComboBox.SelectedValuePath = "SuplidorId";
            SuplidorComboBox.DisplayMemberPath = "Nombres";

            ProductoComboBox.ItemsSource = ProductosBLL.GetList(p => true);
            ProductoComboBox.SelectedValuePath = "ProductoId";
            ProductoComboBox.DisplayMemberPath = "Descripcion";
        }

        private void Limpiar()
        {
            orden = new Ordenes();
            detalle = new List<object>();
            Actualizar();
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = orden;

            DetalleDataGrid.ItemsSource = null;
            DetalleDataGrid.ItemsSource = detalle;
        }

        private void BuscarBoton_Click(object sender, RoutedEventArgs e)
        {
            Ordenes anterior = OrdenesBLL.Buscar(Utilities.ToInt(OrdenIdTextBox.Text));

            if (anterior != null)
            {
                Limpiar();

                orden = anterior;

                foreach(OrdenesDetalle o in orden.Detalle)
                {
                    this.detalle.Add(new
                        {
                            o.OrdenDetalleId,
                            o.OrdenId,
                            o.ProductoId,
                            Producto = ProductosBLL.Buscar(o.ProductoId).Descripcion,
                            o.Cantidad,
                            o.Costo,
                            SubTotal = (decimal)o.Cantidad * o.Costo
                        }
                    );
                }
                Actualizar();
            }
            else
            {
                MessageBox.Show("Orden no encontrada.", "Registro de Ordenes");
            }
        }

        private void AgregarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarDetalle())
                return;

            OrdenesDetalle detalle = new OrdenesDetalle(
                Utilities.ToInt(OrdenIdTextBox.Text),
                Utilities.ToInt(ProductoComboBox.SelectedValue.ToString()),
                Utilities.ToInt(CantidadTextBox.Text),
                Utilities.ToInt(CostoTextBox.Text)
            );

            orden.Detalle.Add(detalle);
            orden.Monto += (decimal)detalle.Cantidad * detalle.Costo;

            this.detalle.Add(new
                {
                    detalle.OrdenDetalleId,
                    detalle.OrdenId,
                    detalle.ProductoId,
                    Producto = ProductosBLL.Buscar(detalle.ProductoId).Descripcion,
                    detalle.Cantidad,
                    detalle.Costo,
                    SubTotal = (decimal)detalle.Cantidad * detalle.Costo
                }
            );

            Actualizar();

            ProductoComboBox.SelectedIndex = -1;
            CostoTextBox.Clear();
            CantidadTextBox.Clear();
        }

        private void RemoverBoton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count > 0 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                object d = DetalleDataGrid.SelectedItem;
                int productoId = Convert.ToInt32(d.GetType().GetProperty("ProductoId").GetValue(d).ToString());
                int ordenDetalleId = Convert.ToInt32(d.GetType().GetProperty("OrdenDetalleId").GetValue(d).ToString());

                OrdenesDetalle detalle = orden.Detalle.Where(p => p.ProductoId == productoId && p.OrdenDetalleId == ordenDetalleId).First();

                orden.Monto -= (decimal)detalle.Cantidad * detalle.Costo;
                orden.Detalle.Remove(detalle);

                this.detalle.Remove(d);

                Actualizar();

            }
        }

        private void NuevoBoton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarBoton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarOrden())
                return;

            bool paso = OrdenesBLL.Guardar(orden);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Orden guardada con éxito.", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No fue posible guardar.", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            int id = Utilities.ToInt(OrdenIdTextBox.Text);

            Limpiar();

            if (OrdenesBLL.Eliminar(id))
                MessageBox.Show("Orden eliminada.", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No se pudo eliminar la orden", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool ValidarDetalle()
        {
            if (!Decimal.TryParse(CostoTextBox.Text, out _))
            {
                MessageBox.Show("Ingrese un costo que contenga un número válido", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (CostoTextBox.Text.Length == 0 || Utilities.ToDecimal(CostoTextBox.Text) == 0)
            {
                MessageBox.Show("Ingrese un costo que sea mayor a cero.", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!float.TryParse(CantidadTextBox.Text, out _))
            {
                MessageBox.Show("Ingrese una cantidad que contenga un número válido", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (CantidadTextBox.Text.Length == 0 || Utilities.ToDecimal(CantidadTextBox.Text) == 0)
            {
                MessageBox.Show("Ingrese una cantidad que sea mayor a cero.", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (ProductoComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un producto", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool ValidarOrden()
        {
            if (!DateTime.TryParse(FechaDatePicker.Text, out _))
            {
                MessageBox.Show("Ingrese una fecha válida", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(SuplidorComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione a un suplidor", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (orden.Detalle.Count == 0)
            {
                MessageBox.Show("Ingrese por lo menos un producto", "Registro de Ordenes", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

    }

}
