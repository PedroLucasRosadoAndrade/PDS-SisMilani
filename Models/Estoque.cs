﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDePDS3_A.Models
{
    internal class Estoque
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Unidade { get; set; }

        public string CodigoEsto { get; set; }

        public string Categoria { get; set; }

        public string EstoqueAnterior { get; set; }

        public string EstoqueAtual { get; set; }

        /*private bool _selected = false;

        public bool IsSelected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }*/
    }
}
