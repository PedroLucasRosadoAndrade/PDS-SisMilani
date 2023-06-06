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
    /// Lógica interna para ListarFuncionarios.xaml
    /// </summary>
    public partial class ListarFuncionarios : Window
    {
        public ListarFuncionarios()
        {
            InitializeComponent();
            Loaded += FuncionarioListWindow_Loaded;
        }

        private void FuncionarioListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
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

        private void MenuItem_Novo_Click(object sender, RoutedEventArgs e)
        {
            var window = new WindowCadastrarFuncionario();
            window.ShowDialog();
            LoadDataGrid();
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            var funcionarioSelected = dataGrid.SelectedItem as Funcionario;

            var window = new WindowCadastrarFuncionario(funcionarioSelected.Id);
            window.ShowDialog();
            LoadDataGrid();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            var funcionarioSelected = dataGrid.SelectedItem as Funcionario;

            var result = MessageBox.Show($"Deseja realmente remover o funcionário `{funcionarioSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new FuncionarioDAO();
                    dao.Delete(funcionarioSelected);
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
