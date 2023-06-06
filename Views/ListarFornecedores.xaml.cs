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
using ProjetoDePDS3_A.Models;

namespace ProjetoDePDS3_A.Views
{
    /// <summary>
    /// Lógica interna para ListarFornecedores.xaml
    /// </summary>
    public partial class ListarFornecedores : Window
    {
        public ListarFornecedores()
        {
            InitializeComponent();
            Loaded += ListarFuncionarios_Loaded;

        }

        private void LoadDataGrid()
        {
            try
            {
                var dao = new FuncionarioDAO();

                dataGrid.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
