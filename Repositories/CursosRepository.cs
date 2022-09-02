using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APICursosGratuitos.Repositories
{
    public class CursosRepository : ICursosRepository
    {
        public CursosRepository(IConfiguration configuration)
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
                string script = "DELETE FROM Cursos WHERE Id=@id";

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

        public ICollection<Cursos> GetAll()
        {
            var cursos = new List<Cursos>(); // Criando um objeto do tipo lista para exibir todas as colunas e dados da tabela Cursos

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                // Escreve a consulta/script de busca
                string consulta = @"SELECT 
                                    C.Id AS 'Id_Cursos',
                                    C.Nome AS 'Nome_Cursos',
                                    C.CargaHoraria AS 'Hora_Cursos',
                                    C.Imagem AS 'Imagem_Cursos',
                                    C.AreaId AS 'Id_Area_Cursos',
                                    A.Area 
                                    FROM Cursos AS C 
                                    INNER JOIN Areas AS A ON C.AreaId = A.Id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Lê todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Usando o laço while 
                        while (reader.Read() && reader != null)
                        {
                            cursos.Add(new Cursos
                            {
                                Id = (int)reader["Id_Cursos"],
                                Nome = (string)(reader["Nome_Cursos"]),
                                CargaHoraria = (int)reader["Hora_Cursos"],
                                Area = new Areas
                                {
                                    Id = (int)reader["Id_Area_Cursos"],
                                    Area = (string)reader[5],
                                    Imagem = (string)reader[6]
                                },
                                Imagem = (string)reader["Imagem_Cursos"]
                            });
                        }
                    }
                }

            }

            return cursos;
        }

        public Cursos GetById(int id)
        {
            Cursos cursos = null; // Objeto criado atraves da classe Cursos

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open(); // abre a conexão

                // Escreve a script de busca por id
                string consulta = "SELECT * FROM Cursos WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            cursos = new Cursos
                            {
                                Id = (int)result[0],
                                Nome = (string)result[1],
                                CargaHoraria = (int)result[2],
                                Area = (Areas)result[3],
                                Imagem = (string)result[4]
                            };

                        }
                    }
                }

            }
            return cursos; // retorna o cursos selecionado através do Id
        }

        public Cursos Insert(Cursos cursos)
        {
            throw new System.NotImplementedException();
        }

        public Cursos Update(int id, Cursos cursos)
        {
            throw new System.NotImplementedException();
        }

        Cursos ICursosRepository.Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
