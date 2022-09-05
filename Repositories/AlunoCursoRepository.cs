using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APICursosGratuitos.Repositories
{
    public class AlunoCursoRepository : IAlunoCursoRepository
    {
        public AlunoCursoRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionCall = Configuration.GetConnectionString("CursosGratuitos"); // String de conexão chamado através do arquivo appsettings.json
        }

        public IConfiguration Configuration { get; set; }
        private string connectionCall { get; set; }

        // Cria uma string de conexão com o Banco de Dados
        //readonly = variável de apenas leitura 
               

        //Deleta uma area através de seu Id
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionCall)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); // abrir conexão

                // escreve a consulta de exclusão
                string script = "DELETE FROM AlunoCurso WHERE Id=@id";

                // Comando de execução no banco criado
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //Declarações das variaveis por parametros
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    // Tipo de comando, tipo texto. CommandType é um Enum
                    cmd.CommandType = CommandType.Text;
                    int linhasAfetadas = cmd.ExecuteNonQuery(); //Retorna os numeros de linhas alteradas/afetadas 
                    if (linhasAfetadas == 0)
                    {
                        return false; // não retorna nada
                    }
                }
            }
            return true;
        }

        public ICollection<AlunoCurso> GetAll()
        {
            var alunoCurso = new List<AlunoCurso>(); // Criando um objeto do tipo lista para exibir todas as colunas e dados da tabela AlunoCurso

            using (SqlConnection conexao = new SqlConnection(connectionCall))
            {
                conexao.Open();

                // Escreve a consulta/script de busca
                string consulta = @"SELECT * FROM AlunoCurso
                                    JOIN Alunos ON AlunoCurso.AlunoRa = Alunos.RA
                                    JOIN Cursos ON AlunoCurso.CursoId = Cursos.Id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            alunoCurso.Add(new AlunoCurso
                            {
                                Id = (int)result[0],
                                AlunoRa = (int)result[1],
                                CursoId = (int)result[2],

                            });
                        }
                    }
                }
            }                       

            return alunoCurso;
        }

        public AlunoCurso GetById(int id)
        {
            var alunoCurso = new AlunoCurso();

            using (SqlConnection conexao = new SqlConnection(connectionCall))
            {
                conexao.Open();

                string script = "SELECT * FROM AlunoCurso WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            alunoCurso.Id = (int)reader[0];
                            alunoCurso.AlunoRa = (int)reader[1];
                            alunoCurso.CursoId = (int)reader[2];


                        }
                    }
                }

            }
            return alunoCurso;
        }

        public AlunoCurso Insert(AlunoCurso alunoCurso)
        {
            using (SqlConnection conexao = new SqlConnection(connectionCall)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open();

                // escrever a nossa consulta
                string script = "INSERT INTO AlunoCurso (AlunoRa, CursoId) VALUES (@AlunoRa, @CursoId)";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazemos as declarações das variaveis por parametros
                    cmd.Parameters.Add("@AlunoRa", SqlDbType.Int).Value = alunoCurso.AlunoRa;
                    cmd.Parameters.Add("@CursoId", SqlDbType.Int).Value = alunoCurso.CursoId;

                    // Tipo de comando, tipo texto. CommandType é um Enum
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

            }

            return alunoCurso;
        }

        public AlunoCurso Update(int id, AlunoCurso alunoCurso)
        {
            using (SqlConnection conexao = new SqlConnection(connectionCall)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open();

                // escrever a nossa consulta
                string script = "UPDATE  AlunoCurso SET AlunoRa = @AlunoRa, CursoId = @CursoId WHERE Id=@id";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazemos as declarações das variaveis por parametros
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@AlunoRa", SqlDbType.Int).Value = alunoCurso.AlunoRa;
                    cmd.Parameters.Add("@CursoId", SqlDbType.Int).Value = alunoCurso.CursoId;

                    // Tipo de comando, tipo texto. CommandType é um Enum
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    alunoCurso.Id = id;
                }

            }

            return alunoCurso;
        }

        AlunoCurso IAlunoCursoRepository.Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
