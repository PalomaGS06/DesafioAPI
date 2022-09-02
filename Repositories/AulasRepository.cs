using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APICursosGratuitos.Repositories
{
    public class AulasRepository : IAulasRepository
    { 
        public AulasRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionCall = Configuration.GetConnectionString("CursosGratuitos"); // String de conexão chamado através do arquivo appsettings.json
        }

        public IConfiguration Configuration { get; set; }
        private string connectionCall { get; set; }

        // Cria uma string de conexão com o Banco de Dados
        //readonly = variável de apenas leitura 

        readonly string connectionString = "Data Source=WORKSTATIONSOUZ\\SQLEXPRESS;Integrated Security=true;Initial Catalog=CursosGratuitos";


        //Deleta uma area através de seu Id
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); // abrir conexão

                // escreve a consulta de exclusão
                string script = "DELETE FROM Aulas WHERE Id=@id";

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

        public ICollection<Aulas> GetAll()
        {
            var aulas = new List<Aulas>(); // Criando um objeto do tipo lista para exibir todas as colunas e dados da tabela Aulas

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                // Escreve a consulta/script de busca
                string consulta = @"SELECT 
                                    A.Id AS 'Id_Aulas',
                                    A.Titulo AS 'Titulo_Aulas',
                                    A.Imenta AS 'Imenta_Aulas',
                                    A.Duracao AS 'Duracao_Aulas',
                                    A.CursoId AS 'Id_Curso_Aulas',
                                    A.ProfessorCpf AS 'Cpf_Prof_Aulas',
                                    FROM Aulas AS A 
                                    INNER JOIN Cursos AS C ON A.CursoId = C.Id
                                    INNER JOIN Professores AS P ON A.ProfessorCpf = P.CPF";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Lê todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Usando o laço while 
                        while (reader.Read() && reader != null)
                        {
                            aulas.Add(new Aulas
                            {
                                Id = (int)reader["Id_Aulas"],
                                Titulo = (string)(reader["Titulo_Aulas"]),
                                Imenta = (string)reader["Imenta_Aulas"],
                                Duracao = (int)reader["Duracao_Aulas"],
                                Curso = new Cursos
                                {
                                    Id = (int)reader["Id_Curso_Aulas"],
                                    Nome = null,
                                    CargaHoraria = 0,
                                    Imagem = null
                                },
                                Professor = new Professores
                                {
                                    Cpf = (int)reader["Cpf_Prof_Aulas"],
                                    Nome = null,
                                    Email = null
                                },
                            }); 
                        }
                    }
                }

            }

            return aulas;
        }

        public Aulas GetById(int id)
        {
            Aulas aulas = null; // Objeto criado atraves da classe Aulas

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open(); // abre a conexão

                // Escreve a script de busca por id
                string consulta = "SELECT * FROM Aulas WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            aulas = new Aulas
                            {
                                Id = (int)result[0],
                                Titulo = (string)result[1],
                                Imenta = (string)result[2],
                                Duracao = (int)result[3],
                                Curso = new Cursos
                                {
                                    Id = (int)result[4],
                                    Nome = null,
                                    CargaHoraria = 0,
                                    Imagem = null
                                },
                                Professor = new Professores
                                {
                                    Cpf = (int)result[8],
                                    Nome = null,
                                    Email = null
                                },
                            };
                        }   
                    }
                }

            }
            return aulas; // retorna a aula selecionada através do Id
        }

        public Aulas Insert(Aulas aulas)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open();   // Abre uma conexao

                // escrever a consulta de inserção
                string script = "INSERT INTO Aulas (Titulo, Imenta, Duracao, CursoId, ProfessorCpf) VALUES (@Titulo, @Imenta, @Duracao, @CursoId, ProfessorCpf)";

                // O comando de execução no banco é criado
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //As declarações das variaveis por parametros são feitas
                    cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar).Value = aulas.Titulo;
                    cmd.Parameters.Add("@Imenta", SqlDbType.NVarChar).Value = aulas.Imenta;
                    cmd.Parameters.Add("@Duracao", SqlDbType.Int).Value = aulas.Duracao;
                    cmd.Parameters.Add("@AreaId", SqlDbType.Int).Value = aulas?.Curso?.Id ?? 0;
                    cmd.Parameters.Add("@Imagem", SqlDbType.Int).Value = aulas?.Professor?.Cpf ?? 0;

                    cmd.CommandType = CommandType.Text; // Tipo de comando Enum, do tipo texto.
                    cmd.ExecuteNonQuery(); //returna o número de linhas afetadas
                }
            }

            return aulas; //retorna a aula criada
        }

        public Aulas Update(int id, Aulas aulas)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); //conexão iniciada

                // escrever a consulta de atualização dos dados
                string script = "UPDATE Aulas SET Titulo=@Titulo, Imenta=@Imenta WHERE Id=@id";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //Declarações das variáveis por parametro
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = aulas.Id;
                    cmd.Parameters.Add("@Titulo", SqlDbType.NVarChar).Value = aulas.Titulo;
                    cmd.Parameters.Add("@Imenta", SqlDbType.NVarChar).Value = aulas.Imenta;

                    // Tipo de comando, tipo texto
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return aulas; //campo alterado 
        }

        Aulas IAulasRepository.Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
