using MySql.Data.MySqlClient;
using ProjetoDePDS3_A.DataBase;
using ProjetoDePDS3_A.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDePDS3_A.Models
{
    internal class EstoqueDAO : IDAO<Estoque>
    {
        private Conexao conn;

        public EstoqueDAO()
        {
            conn = new Conexao();
        }

        public void Delete(Produto t)
        {
            throw new NotImplementedException();
        }

        public Estoque GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Estoque t)
        {
            throw new NotImplementedException();
        }

        public List<Estoque> List()
        {
            try
            {
                List<Estoque> list = new List<Estoque>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM produto";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Estoque()
                    {
                        Id = reader.GetInt32("cod_prod"),
                        Nome = reader.GetString("nome_prod"),
                        Unidade = reader.GetString("unidade_prod"),
                        ValorCompra = reader.GetDouble("valor_compra_prod")
                    });
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Estoque t)
        {
            throw new NotImplementedException();
        }
    }
}
