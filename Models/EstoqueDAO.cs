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

        public void Delete(Estoque t)
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
                query.CommandText = "SELECT * FROM Estoque";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Estoque()
                    {
                        Id = reader.GetInt32("id_est"),
                        Nome = reader.GetString("unidade_est"),
                        Unidade = reader.GetString("cod_est"),
                        CodigoEsto = reader.GetString("categoria_est"),
                        Categoria = reader.GetString("data_est"),
                        EstoqueAnterior = reader.GetString("valor_est"),
                        EstoqueAtual = reader.GetString("valor_est")
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
