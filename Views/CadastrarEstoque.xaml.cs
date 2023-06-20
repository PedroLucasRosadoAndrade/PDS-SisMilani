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
using MySqlX.XDevAPI;
using System.Runtime.Remoting.Messaging;
using System.Windows.Media.TextFormatting;
using ProjetoDePDS3_A.Models;
using ProjetoDePDS3_A.Helpers;

namespace ProjetoDePDS3_A.Views
{
    /// <summary>
    /// Lógica interna para CadastrarEstoque.xaml
    /// </summary>
    public partial class CadastrarEstoque : Window
    {
        private int _id;

        private Estoque _estoque;
        public CadastrarEstoque()
        {
            InitializeComponent();
            Loaded += CadastrarEstoque_Loaded;
        }

        public CadastrarEstoque(int id)
        {
            _id = id;
            InitializeComponent();
            Loaded += CadastrarEstoque_Loaded;
        }

        private void CadastrarEstoque_Loaded(object sender, RoutedEventArgs e)
        {
            _estoque = new Estoque();

            LoadComboBox();

            if (_id > 0)
                FillForm();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _estoque.Nome = txtNome.Text;
            _estoque.Unidade = txtUnidade.Text;
            _estoque.CodigoEsto = txtCodigo.Text;
            _estoque.Categoria = txtCategoria.Text;
            _estoque.EstoqueAnterior = TxtEstoAnt.Text;
            _estoque.EstoqueAtual = txtEstoAtu.Text;
            
           


            /*if (double.TryParse(txtSalario.Text, out double salario))
                _cliente.Salario = salario;*/

            /*if (dtPickerDataNascimento.SelectedDate != null)
                _estoque.DataNascimento = (DateTime)dtPickerDataNascimento.SelectedDate;*/

            /*if (comboBoxSexo.SelectedItem != null)
                _cliente.Sexo = comboBoxSexo.SelectedItem as Sexo;*/

            /*_cliente.Endereco = new Endereco();
            _cliente.Endereco.Rua = TxtRua.Text;
            _cliente.Endereco.Bairro = txtBairro.Text;
            _cliente.Endereco.Cidade = txtCidade.Text;

            if (int.TryParse(txtNumero.Text, out int numero))
                _cliente.Endereco.Numero = numero;*/

            /*if (txtEstado.SelectedItem != null)
                _estoque.Endereco.Estado = txtEstado.SelectedItem as string;*/

            SaveData();
        }

        private bool Validate()
        {
            var validator = new EstoqueValidator();
            var result = validator.Validate(_estoque);

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
                    var dao = new ClienteDAO();
                    var text = "atualizado";

                    if (_estoque.Id == 0)
                    {
                        dao.Insert(_estoque);
                        text = "adicionado";
                    }
                    else
                        dao.Update(_estoque);

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
                var dao = new ClienteDAO();
                _estoque = dao.GetById(_id);

                //txtId.Text = _cliente.Id.ToString();
                txtNome.Text = _estoque.Nome;
                txtCpf.Text = _estoque.CPF;
                TxtRG.Text = _estoque.RG;
                dtPickerDataNascimento.SelectedDate = _estoque.DataNascimento;
                txtEmail.Text = _estoque.Email;
                txtTelefone.Text = _estoque.Telefone;
                txtEmail.Text = _estoque.Email;
                txtCep.Text = _estoque.CEP;

                /*if (_cliente.Sexo != null)
                    comboBoxSexo.SelectedValue = _cliente.Sexo.Id;*/

                if (_estoque.Endereco != null)
                {
                    TxtRua.Text = _estoque.Endereco.Rua;
                    TxtSenha.Text = _estoque.Endereco.Numero.ToString();
                    TxtRua.Text = _estoque.Endereco.Rua;
                    txtCep.Text = _estoque.Endereco.Cidade;

                    txtEstado.SelectedValue = _estoque.Endereco.Estado;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseFormVerify()
        {
            if (_estoque.Id == 0)
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
                txtEstado.ItemsSource = Estado.List();
                //comboBoxSexo.ItemsSource = new SexoDAO().List();
            }
            catch (Exception ex)
            {

            }
        }

        private void ClearInputs()
        {
            txtNome.Text = "";
            txtCpf.Text = "";
            TxtRG.Text = "";
            dtPickerDataNascimento.SelectedDate = null;
            txtEmail.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            txtCep.Text = "";

        }
    }
}
