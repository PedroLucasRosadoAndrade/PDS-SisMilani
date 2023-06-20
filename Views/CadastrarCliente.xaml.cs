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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using ProjetoDePDS3_A.Models;
using ProjetoDePDS3_A.Helpers;
using MySqlX.XDevAPI;
using System.Runtime.Remoting.Messaging;

namespace ProjetoDePDS3_A.Views
{
    /// <summary>
    /// Lógica interna para CadastrarCliente.xaml
    /// </summary>
    public partial class CadastrarCliente : Window
    {
        private int _id;

        private Cliente _cliente;

        public CadastrarCliente()
        {
            InitializeComponent();
            Loaded += CadastrarCliente_Loaded;
        }

        public CadastrarCliente(int id)
        {
            _id = id;
            InitializeComponent();
            Loaded += CadastrarCliente_Loaded;
        }

        private void CadastrarCliente_Loaded(object sender, RoutedEventArgs e)
        {
            _cliente = new Cliente();

            LoadComboBox();

            if (_id > 0)
                FillForm();
        }

        private void btLimpar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            _cliente.Nome = txtNome.Text;
            _cliente.RG = TxtRG.Text;
            _cliente.Cidade = txtCidade.Text;
            _cliente.UF = txtUF.Text;
            _cliente.Telefone = txtTelefone.Text;
            _cliente.Email = txtEmail.Text;
            _cliente.CEP = txtCep.Text;
            _cliente.CPF = txtCpf.Text;
            _cliente.Rua = TxtRua.Text;
            _cliente.Senha = TxtSenha.Text;

            
            /*if (double.TryParse(txtSalario.Text, out double salario))
                _cliente.Salario = salario;*/

            if (dtPickerDataNascimento.SelectedDate != null)
                _cliente.DataNascimento = (DateTime)dtPickerDataNascimento.SelectedDate;

            /*if (comboBoxSexo.SelectedItem != null)
                _cliente.Sexo = comboBoxSexo.SelectedItem as Sexo;*/

            /*_cliente.Endereco = new Endereco();
            _cliente.Endereco.Rua = TxtRua.Text;
            _cliente.Endereco.Bairro = txtBairro.Text;
            _cliente.Endereco.Cidade = txtCidade.Text;

            if (int.TryParse(txtNumero.Text, out int numero))
                _cliente.Endereco.Numero = numero;*/

            if (txtCidade.SelectedItem != null)
                _cliente.Endereco.Cidade = txtCidade.SelectedItem as string;

            SaveData();
        }

        private bool Validate()
        {
            var validator = new ClienteValitator();
            var result = validator.Validate(_cliente);

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

                    if (_cliente.Id == 0)
                    {
                        dao.Insert(_cliente);
                        text = "adicionado";
                    }
                    else
                        dao.Update(_cliente);

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
                _cliente = dao.GetById(_id);

                //txtId.Text = _cliente.Id.ToString();
                txtNome.Text = _cliente.Nome;
                txtCpf.Text = _cliente.CPF;
                TxtRG.Text = _cliente.RG;
                dtPickerDataNascimento.SelectedDate = _cliente.DataNascimento;
                txtEmail.Text = _cliente.Email;
                txtTelefone.Text = _cliente.Telefone;
                txtEmail.Text = _cliente.Email;
                txtCep.Text = _cliente.CEP;
                txtUF.Text = _cliente.UF;
                txtCidade.Text = _cliente.Cidade;

                /*if (_cliente.Sexo != null)
                    comboBoxSexo.SelectedValue = _cliente.Sexo.Id;*/

                if (_cliente.Endereco != null)
                {
                    TxtRua.Text = _cliente.Endereco.Rua;
                    TxtSenha.Text = _cliente.Endereco.Numero.ToString();
                    TxtRua.Text = _cliente.Endereco.Rua;
                    txtCep.Text = _cliente.Endereco.Cidade;

                    txtCidade.SelectedValue = _cliente.Endereco.Estado;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CloseFormVerify()
        {
            if (_cliente.Id == 0)
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
                txtCidade.ItemsSource = Estado.List();
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
            txtUF.Text = "";
            txtCidade.Text = "";


        }

        
    }
}
