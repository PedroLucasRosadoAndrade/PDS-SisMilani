using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDePDS3_A.Interfaces;
using ProjetoDePDS3_A.DataBase;
using ProjetoDePDS3_A.Helpers;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace ProjetoDePDS3_A.Models
{
    internal class ClienteDAO : IDAO<Cliente>
    {

        private static Conexao conn;
        public ClienteDAO() {
            conn = new Conexao();
        }
       
        public void Delete(Cliente t)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM cliente WHERE cod_func = @id";

                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Registro não removido da base de dados. Verifique e tente novamente.");

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

        public Cliente GetById(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "SELECT * FROM cliente " +
                                                "LEFT JOIN sexo ON cod_sex = cod_sex_fk " +
                                                "LEFT JOIN endereco ON cod_end = cod_end_fk " +
                                                "WHERE cod_func = @id";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                    throw new Exception("Nenhum registro foi encontrado!");

                var cliente = new Cliente();

                while (reader.Read())
                {
                    cliente.Id = reader.GetInt32("id_cli");
                    cliente.Nome = reader.GetString("nome_cli");
                    cliente.RG = reader.GetString("rg_cli");
                    cliente.UF = reader.GetString("uf_cli");
                    cliente.Telefone = reader.GetString("telefone_cli");
                    cliente.Email = reader.GetString("email_cli");
                    cliente.CEP = reader.GetString("cep_cli");
                    cliente.DataNascimento = DAOHelper.GetDateTime(reader, "data_nasc_cli");
                    cliente.CPF = reader.GetString("cpf_cli");
                    cliente.Rua = reader.GetString("rua_cli");
                    cliente.Senha = reader.GetString("senha_cli");
                    


                    if (!DAOHelper.IsNull(reader, "cod_sex_fk"))
                        cliente.Sexo = new Sexo()
                        {
                            Id = reader.GetInt32("cod_sex"),
                            Nome = reader.GetString("nome_sex")
                        };

                    if (!DAOHelper.IsNull(reader, "cod_end_fk"))
                        cliente.Endereco = new Endereco()
                        {
                            Id = reader.GetInt32("cod_end"),
                            Rua = reader.GetString("rua_end"),
                            Numero = reader.GetInt32("numero_end"),
                            Bairro = reader.GetString("bairro_end"),
                            Cidade = reader.GetString("cidade_end"),
                            Estado = reader.GetString("estado_end")
                        };
                }

                return cliente;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Query();
            }
        }

        public void Insert(Cliente t)
        {
            try
            {
                //Inserção do Endereço do Funcionário
                var enderecoId = new EnderecoDAO().Insert(t.Endereco);



                /*
                 * PROCEDURE `inserir_funcionario`(IN nome varchar(200), IN cpf varchar(20), IN rg varchar(20),
                                                     IN datanasc date, IN email varchar(200), IN celular varchar(50), 
                                                    IN funcao varchar(50),
                                                     IN salario double)
                 */
                var query = conn.Query();
                query.CommandText = "INSERT INTO cliente " +
                    "(id_cli, nome_cli, rg_cli, uf_cli, telefone_cli, email_cli, cep_cli, data_nasc_cli, cpf_cli, rua_cli, senha_cli, sexo_cli, id_log_fk, id_ing_fk, id_end_fk) " +
                    "VALUES (@id, @nome, @rg, @uf,  @telefone, @email, @cep, @datanasc, @cpf, @rua, @senha, @sexo, @login, @ingreco, @enderecoId)";

                //query.CommandText = "CALL inserir_funcionario(@nome, @sexo, @cpf, @rg, @datanasc, @email, @celular, @funcao, @salario)";

                query.Parameters.AddWithValue("@id", t.Id);
                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@uf", t.UF);
                query.Parameters.AddWithValue("@telefone", t.Telefone);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@cep", t.CEP);
                query.Parameters.AddWithValue("@datanasc", t.DataNascimento?.ToString("yyyy-MM-dd")); //"10/11/1990" -> "1990-11-10"
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@rua", t.Rua);
                query.Parameters.AddWithValue("@senha", t.Senha);
                query.Parameters.AddWithValue("@sexoId", t.Sexo.Id);
                query.Parameters.AddWithValue("@login", t.Id_log);
                query.Parameters.AddWithValue(" @ingreco", t.Id_Ing);
                query.Parameters.AddWithValue("@enderecoId", enderecoId);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("O registro não foi inserido. Verifique e tente novamente");

                /*
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    if(reader.GetName(0).Equals("Alerta"))
                    {
                        throw new Exception(reader.GetString("Alerta"));
                    }
                }
                */

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

        public List<Cliente> List()
        {
            try
            {
                List<Cliente> list = new List<Cliente>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM Cliente LEFT JOIN sexo ON cod_sex = cod_sex_fk";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Cliente()
                    {
                        Id = reader.GetInt32("id_cli"),
                        Nome = DAOHelper.GetString(reader, "nome_cli"),
                        RG = DAOHelper.GetString(reader, "rg_cli"),
                        UF = DAOHelper.GetString(reader, "uf_cli"),
                        Telefone = DAOHelper.GetString(reader, "telefone_cli"),
                        Email = DAOHelper.GetString(reader, "email_cli"),
                        CEP = DAOHelper.GetString(reader, "cep_cli"),
                        DataNascimento = DAOHelper.GetDateTime(reader, "data_nasc_cli"),
                        CPF = DAOHelper.GetString(reader, "cpf_cli"),
                        Rua = DAOHelper.GetString(reader, "rua_cli"),
                        Senha = DAOHelper.GetString(reader, "senha_cli"),
                        Sexo = DAOHelper.IsNull(reader, "cod_sex_fk") ? null : new Sexo() { Id = reader.GetInt32("cod_sex"), Nome = reader.GetString("nome_sex") }

                        


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

        public void Update(Cliente t)
        {
            try
            {
                long enderecoId = t.Endereco.Id;
                var endDAO = new EnderecoDAO();

                if (enderecoId > 0)
                    endDAO.Update(t.Endereco);
                else
                    enderecoId = endDAO.Insert(t.Endereco);

                var query = conn.Query();
                query.CommandText = "UPDATE funcionario SET nome_func = @nome, cpf_func = @cpf, rg_func = @rg, datanasc_func = @datanasc, " +
                    "email_func = @email, celular_func = @celular, funcao_func = @funcao, salario_func = @salario, " +
                    "cod_sex_fk = @sexo, cod_end_fk = @enderecoId WHERE cod_func = @id";

                query.Parameters.AddWithValue("@id", t.Id);
                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@uf", t.UF);
                query.Parameters.AddWithValue("@telefone", t.Telefone);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@cep", t.CEP);
                query.Parameters.AddWithValue("@datanasc", t.DataNascimento?.ToString("yyyy-MM-dd")); //"10/11/1990" -> "1990-11-10"
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@rua", t.Rua);
                query.Parameters.AddWithValue("@senha", t.Senha);
                query.Parameters.AddWithValue("@sexoId", t.Sexo.Id);
                query.Parameters.AddWithValue("@login", t.Id_log);
                query.Parameters.AddWithValue(" @ingreco", t.Id_Ing);
                query.Parameters.AddWithValue("@enderecoId", enderecoId);




                query.Parameters.AddWithValue("@id", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Atualização do registro não foi realizada.");

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

    }
}
