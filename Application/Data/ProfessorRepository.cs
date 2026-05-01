using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class ProfessorRepository
    {
        private readonly string _connection;

        public ProfessorRepository()
        {
            var conn = ConfigurationManager.ConnectionStrings["DEV"]?.ConnectionString;

            if (string.IsNullOrEmpty(conn))
                throw new Exception("Connection string 'DEV' não encontrada no App.config.");

            _connection = conn;
        }

        public bool Salvar(Professor professor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    string query = "INSERT INTO dbo.Professor (Nome, Email, Titulo) VALUES (@Nome, @Email, @Titulo)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = professor.Nome;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(professor.Email) ? (object)DBNull.Value : professor.Email;
                        cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(professor.Titulo) ? (object)DBNull.Value : professor.Titulo;

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar professor", ex);
            }
        }

        public List<Professor> Listar()
        {
            List<Professor> professores = new List<Professor>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connection))
                {
                    conn.Open();

                    string query = "SELECT ProfessorId, Nome, Email, Titulo FROM dbo.Professor";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Professor professor = new Professor
                            {
                                ProfessorId = Convert.ToInt32(reader["ProfessorId"]),
                                Nome = reader["Nome"].ToString(),
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                Titulo = reader["Titulo"] != DBNull.Value ? reader["Titulo"].ToString() : null
                            };

                            professores.Add(professor);
                        }
                    }
                }

                return professores;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar professores", ex);
            }
        }
    }
}