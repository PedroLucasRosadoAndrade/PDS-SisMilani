using ProjetoDePDS3_A.Models;
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
using System.Windows.Shapes;

namespace ProjetoDePDS3_A.Views
{
    /// <summary>
    /// Lógica interna para ListarFilme.xaml
    /// </summary>
    
       
        public partial class ListarFilme : Window
        {
            public ListarFilme()
            {
                InitializeComponent();
                Loaded += CadastrarFilme_Loaded;
            }

            private void CadastrarFilme_Loaded(object sender, RoutedEventArgs e)
            {
                LoadDataGrid();
            }

            private void LoadDataGrid()
            {
                try
                {
                    var dao = new ClienteDAO();

                    dataGrid.ItemsSource = dao.List();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void Button_Delete_Click(object sender, RoutedEventArgs e)
            {
                var clienteSelected = dataGrid.SelectedItem as Cliente;

                var result = MessageBox.Show($"Deseja realmente remover o funcionário `{clienteSelected.Nome}`?", "Confirmação de Exclusão",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        var dao = new ClienteDAO();
                        dao.Delete(clienteSelected);
                        LoadDataGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }
    
}
