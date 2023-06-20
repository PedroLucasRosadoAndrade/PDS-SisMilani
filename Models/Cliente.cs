using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDePDS3_A.Models
{
    internal class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string RG { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string CEP { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string CPF { get; set; }

        public string Rua { get; set; }

        public string Senha { get; set; }

        public Sexo Sexo { get; set; }

        public string Bairro { get; set; }

        public int Id_log { get; set; }

        public int Id_Ing { get; set; }
        public Endereco Endereco { get; set; }

    }
}
