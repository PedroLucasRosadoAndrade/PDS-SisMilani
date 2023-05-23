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
    /// Lógica interna para ListarCadastros.xaml
    /// </summary>
    public partial class ListarCadastros : Window
    {
        public ListarCadastros()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListarFornecedores form = new ListarFornecedores();
            form.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ListarFilme form =  new ListarFilme();
            form.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ListarFuncionarios form = new ListarFuncionarios();
            form.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
             ListarProdutores form = new ListarProdutores();    
            form.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ListarCliente form = new ListarCliente();   
            form.Show();
        }
    }
}
