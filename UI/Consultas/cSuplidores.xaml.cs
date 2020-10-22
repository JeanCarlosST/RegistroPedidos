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
    /// Interaction logic for cSuplidores.xaml
    /// </summary>
    public partial class cSuplidores : Window
    {
        public cSuplidores()
        {
            InitializeComponent();
        }

        private void ConsultarBoton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Suplidores>();

            string criterio = CriterioTextBox.Text.Trim();
            if (criterio.Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = SuplidoresBLL.GetList(p => p.SuplidorId == Utilities.ToInt(CriterioTextBox.Text));
                        break;

                    case 1:
                        listado = SuplidoresBLL.GetList(p => p.Nombres.ToLower().Contains(criterio.ToLower()));
                        break;

                }
            }
            else
            {
                listado = SuplidoresBLL.GetList(c => true);
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
