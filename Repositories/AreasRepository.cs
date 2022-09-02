using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APICursosGratuitos.Repositories
{
    public class AreasRepository : IAreasRepository
    {
        public AreasRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionCall = Configuration.GetConnectionString("CursosGratuitos"); // String de conexão chamado através do arquivo appsettings.json
        }

        public IConfiguration Configuration { get; set; }
        private string connectionCall { get; set; }

        // Cria uma string de conexão com o Banco de Dados
        //variável de apenas leitura = readonly

        readonly string connectionString = "Data Source=WORKSTATIONSOUZ\\SQLEXPRESS;Integrated Security=true;Initial Catalog=CursosGratuitos";

        //Deleta uma area através de seu Id
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); // abrir conexão

                // escreve a consulta de exclusão
                string script = "DELETE FROM Areas WHERE Id=@id";

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

        //Busca todos os dados
        public ICollection<Areas> GetAll()
        {
            var areas = new List<Areas>(); // Criando um objeto do tipo lista para exibir todas as colunas e dados da tabela Areas

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                // Escreve a consulta/script de busca
                string consulta = @"SELECT A.Id AS 'Id_Areas',
                                           A.Area AS 'Area',
                                           A.Imagem AS 'Img_Areas'
                                    FROM Areas AS A";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Colunas lidas através de um laço condicional
                        while (reader.Read())
                        {
                            areas.Add(new Areas
                            {
                                // Cada coluna, criada pelo Model, representa uma coluna da tabela Areas, contendo seus respectivos tipo de dado e índice da array
                                Id = (int)reader["Id_Areas"],
                                Area = (string)reader["Area"],
                                Imagem = (string)reader["Img_Areas"]
                            });
                        }
                    }
                }

            }

            return areas; // retorna a lista areas
        }

        //Busca area pelo Id
        public Areas GetById(int id)
        {
            Areas areas = null; // Objeto criado atraves da classe Areas

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open(); // abre a conexão

                // Escreve a script de busca por id
                string consulta = "SELECT * FROM Areas WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    // Ler todos os itens da consulta
                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            areas = new Areas
                            {
                                Id = (int)result[0],
                                Area = (string)result[1],
                                Imagem = (string)result[2]
                            };

                        }
                    }
                }

            }
            return areas; // retorna a area selecionada através do Id
        }

        //Cria uma area
        public Areas Insert(Areas areas)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open();   // Abre uma conexao

                // escrever a consulta de inserção
                string script = "INSERT INTO Areas (Area, Imagem) VALUES (@Area, @Imagem)";

                // O comando de execução no banco é criado
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //As declarações das variaveis por parametros são feitas
                    cmd.Parameters.Add("@Area", SqlDbType.NVarChar).Value = areas.Area;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = areas.Imagem;

                    cmd.CommandType = CommandType.Text; // Tipo de comando Enum, do tipo texto.
                    cmd.ExecuteNonQuery(); //returna o número de linhas afetadas
                }
            }

            return areas; //retorna a area criada
        }


        //Altera os dados da classe Areas
        public Areas Update(int id, Areas areas)
        {
            using (SqlConnection conexao = new SqlConnection(connectionString)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); //conexão iniciada

                // escrever a consulta de atualização dos dados
                string script = "UPDATE Areas SET Area=@Area, Imagem=@Imagem WHERE Id=@id";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //Declarações das variáveis por parametro
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = areas.Id;                    
                    cmd.Parameters.Add("@Area", SqlDbType.NVarChar).Value = areas.Area;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = areas.Imagem;

                    // Tipo de comando, tipo texto
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return areas; //campo alterado 
        }

        Areas IAreasRepository.Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
