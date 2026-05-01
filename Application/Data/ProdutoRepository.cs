using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class ProdutoRepository
    {
        private readonly string _connection;

        public ProdutoRepository()
        {
            var conn = ConfigurationManager.ConnectionStrings["DEV"]?.ConnectionString;

            if (string.IsNullOrEmpty(conn))
                throw new Exception("Connection string 'DEV' não encontrada.");

            _connection = conn;
        }

        public ProdutoRepository(string connection)
        {
            _connection = connection;
        }

        public bool Salvar(Produto produto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    // CORREÇÃO: Tabela dbo.Produto e colunas com nomes atualizados
                    string query = "INSERT INTO dbo.Produto (Nome, Preco) VALUES (@Nome, @Preco)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = produto.Nome;
                        cmd.Parameters.Add("@Preco", SqlDbType.Decimal).Value = produto.Preco;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar produto", ex);
            }
        }

        public bool Atualizar(Produto produto)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    // CORREÇÃO: Tabela dbo.Produto e ProdutoId no WHERE
                    string query = "UPDATE dbo.Produto SET Nome = @Nome, Preco = @Preco WHERE ProdutoId = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = produto.Id;
                        cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = produto.Nome;
                        cmd.Parameters.Add("@Preco", SqlDbType.Decimal).Value = produto.Preco;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar produto", ex);
            }
        }

        public bool Excluir(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    // CORREÇÃO: Tabela dbo.Produto e ProdutoId no WHERE
                    string query = "DELETE FROM dbo.Produto WHERE ProdutoId = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir produto", ex);
            }
        }

        public List<Produto> Listar()
        {
            List<Produto> produtos = new List<Produto>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    // CORREÇÃO: Tabela dbo.Produto e leitura das colunas corretas
                    string query = "SELECT ProdutoId, Nome, Preco FROM dbo.Produto";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Produto produto = new Produto
                            {
                                // CORREÇÃO: Lendo "ProdutoId", "Nome" e "Preco" exatamente como está no banco
                                Id = Convert.ToInt32(reader["ProdutoId"]),
                                Nome = reader["Nome"].ToString(),
                                Preco = Convert.ToDecimal(reader["Preco"])
                            };

                            produtos.Add(produto);
                        }
                    }
                }

                return produtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar produtos", ex);
            }
        }
    }
}