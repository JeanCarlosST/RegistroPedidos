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

namespace RegistroPedidos.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cOrdenes.xaml
    /// </summary>
    public partial class cOrdenes : Window
    {
        public cOrdenes()
        {
            InitializeComponent();
        }

        private void ConsultarBoton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Ordenes>();

            string criterio = CriterioTextBox.Text.Trim();
            if (criterio.Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = OrdenesBLL.GetList(p => p.OrdenId == Utilities.ToInt(CriterioTextBox.Text));
                        break;

                    case 1:
                        listado = OrdenesBLL.GetList(p => p.SuplidorId == Utilities.ToInt(CriterioTextBox.Text));
                        break;

                }
            }
            else
            {
                listado = OrdenesBLL.GetList(c => true);
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
