using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class AlunoRepository
    {
        private readonly string _connection;

        public AlunoRepository()
        {
            var conn = ConfigurationManager.ConnectionStrings["DEV"]?.ConnectionString;

            if (string.IsNullOrEmpty(conn))
                throw new Exception("Connection string 'DEV' não encontrada no App.config.");

            _connection = conn;
        }

        public bool Salvar(Aluno aluno)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    string query = "INSERT INTO dbo.Aluno (Nome, Email, DataNascimento) VALUES (@Nome, @Email, @DataNascimento)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = aluno.Nome;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aluno.Email) ? (object)DBNull.Value : aluno.Email;
                        cmd.Parameters.Add("@DataNascimento", SqlDbType.Date).Value = aluno.DataNascimento.HasValue ? (object)aluno.DataNascimento.Value : DBNull.Value;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar aluno", ex);
            }
        }

        public bool Atualizar(Aluno aluno)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    string query = "UPDATE dbo.Aluno SET Nome = @Nome, Email = @Email, DataNascimento = @DataNascimento WHERE AlunoId = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = aluno.AlunoId;
                        cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = aluno.Nome;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(aluno.Email) ? (object)DBNull.Value : aluno.Email;
                        cmd.Parameters.Add("@DataNascimento", SqlDbType.Date).Value = aluno.DataNascimento.HasValue ? (object)aluno.DataNascimento.Value : DBNull.Value;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar aluno", ex);
            }
        }

        public bool Excluir(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    string query = "DELETE FROM dbo.Aluno WHERE AlunoId = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir aluno", ex);
            }
        }

        public List<Aluno> Listar()
        {
            List<Aluno> alunos = new List<Aluno>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    string query = "SELECT AlunoId, Nome, Email, DataNascimento, CreatedAt FROM dbo.Aluno";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aluno aluno = new Aluno
                            {
                                AlunoId = Convert.ToInt32(reader["AlunoId"]),
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                DataNascimento = reader["DataNascimento"] != DBNull.Value ? Convert.ToDateTime(reader["DataNascimento"]) : (DateTime?)null,
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            };

                            alunos.Add(aluno);
                        }
                    }
                }

                return alunos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar alunos", ex);
            }
        }
    }
}