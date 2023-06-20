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
using ProjetoDePDS3_A.Helpers;
using System.Windows.Media.TextFormatting;

namespace ProjetoDePDS3_A.Views
{
    /// <summary>
    /// Lógica interna para CadastrarFilme.xaml
    /// </summary>
    public partial class CadastrarFilme : Window
    {
        private int _id;

        private Filmes _filmes;
        public CadastrarFilme()
        {
            InitializeComponent();
            Loaded += CadastrarFilme_Loaded;
        }
        public CadastrarFilme(int id)
        {
            _id = id;
            InitializeComponent();
            Loaded += CadastrarFilme_Loaded;
        }

        private void CadastrarFilme_Loaded(object sender, RoutedEventArgs e)
        {
            _filmes = new Filmes();

            LoadComboBox();

            if (_id > 0)
                FillForm();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _filmes.Nome = txtNome.Text;
            _filmes.Descricao = txtDescricao.Text;
            _filmes.Valor = txtValor.Text;
            _filmes.Codigo = txtCodigo.Text;
            _filmes.Fornecedor = txtFornecedor.Text;
            _filmes.Unidade = txtUnidade.Text;

            

            if (dtPickerDataFilme.SelectedDate != null)
                _filmes.DataFilme = (DateTime)dtPickerDataFilme.SelectedDate;

           

            

            SaveData();
        }
        private bool Validate()
        {
            var validator = new FilmeValitator;
            var result = validator.Validate(_filmes);

            if (!result.IsValid)
            {
                string errors = null;
                var count = 1;

                foreach (var failure in result.Errors)
                {
                    errors += $"{count++} - {failure.ErrorMessage}\n";
                }

                MessageBox.Show(errors, "Validação de Dados", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return result.IsValid;
        }

        private void SaveData()
        {
            try
            {
                if (Validate())
                {
                    var dao = new FuncionarioDAO();
                    var text = "atualizado";

                    if (_filmes.Id == 0)
                    {
                        dao.Insert(_filmes);
                        text = "adicionado";
                    }
                    else
                        dao.Update(_filmes);

                    MessageBox.Show($"O Funcionário foi {text} com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseFormVerify();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillForm()
        {
            try
            {
                var dao = new FuncionarioDAO();
                _filmes = dao.GetById(_id);

                txtId.Text = _filmes.Id.ToString();
                txtNome.Text = _filmes.Nome;
                txtCPF.Text = _filmes.CPF;
                txtRG.Text = _filmes.RG;
                dtPickerDataNascimento.SelectedDate = _filmes.DataNascimento;
                txtEmail.Text = _filmes.Email;
                txtCelular.Text = _filmes.Celular;
                txtFuncao.Text = _filmes.Funcao;
                txtSalario.Text = _filmes.Salario.ToString();

                if (_filmes.Sexo != null)
                    comboBoxSexo.SelectedValue = _filmes.Sexo.Id;

                if (_filmes.Endereco != null)
                {
                    txtRua.Text = _filmes.Endereco.Rua;
                    txtNumero.Text = _filmes.Endereco.Numero.ToString();
                    txtBairro.Text = _filmes.Endereco.Bairro;
                    txtCidade.Text = _filmes.Endereco.Cidade;

                    comboBoxEstado.SelectedValue = _filmes.Endereco.Estado;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseFormVerify()
        {
            if (_filmes.Id == 0)
            {
                var result = MessageBox.Show("Deseja continuar adicionando funcionários?", "Continuar?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                    this.Close();
                else
                    ClearInputs();
            }
            else
                this.Close();
        }

        private void LoadComboBox()
        {
            try
            {
                comboBoxEstado.ItemsSource = Estado.List();
                comboBoxSexo.ItemsSource = new SexoDAO().List();
            }
            catch (Exception ex)
            {

            }
        }

        private void ClearInputs()
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtRG.Text = "";
            dtPickerDataNascimento.SelectedDate = null;
            txtEmail.Text = "";
            txtCelular.Text = "";
            txtFuncao.Text = "";
            txtSalario.Text = "";
        }

       
    }
}
