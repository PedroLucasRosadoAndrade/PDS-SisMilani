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

namespace ProjetoDePDS3_A
{
    /// <summary>
    /// Lógica interna para TelaInicial.xaml
    /// </summary>
    public partial class TelaInicial : Window
    {
        public TelaInicial()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CadastrarCliente form = new CadastrarCliente();
            form.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CadastrarFilme form = new CadastrarFilme();
            form.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CadastrarFornecedor form = new CadastrarFornecedor();
            form.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CadastrarProdutora form = new CadastrarProdutora();
            form.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WindowCadastrarFuncionario form = new WindowCadastrarFuncionario();
            form.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ListarCadastros form = new ListarCadastros();
            form.Show();
        }
    }
}
