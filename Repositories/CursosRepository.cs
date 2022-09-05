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

        //Deleta uma area através de seu Id
        public bool Delete(int id)
        {
            using (SqlConnection conexao = new SqlConnection(connectionCall)) // dentro do parametro se passa a string de conexao
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

        //Lista tudo
        public ICollection<Cursos> GetAll()
        {
            var cursos = new List<Cursos>(); // Criando um objeto do tipo lista para exibir todas as colunas e dados da tabela Cursos

            using (SqlConnection conexao = new SqlConnection(connectionCall))
            {
                conexao.Open();

                // Escreve a consulta/script de busca
                string consulta = @"SELECT 
                                    C.Id AS 'Id_Cursos',
                                    C.Nome AS 'Nome_Cursos',
                                    C.CargaHoraria AS 'Hora_Cursos',
                                    C.Imagem AS 'Imagem_Cursos',
                                    C.AreaId AS 'Id_Area_Cursos',
                                    A.Area,
                                    A.Imagem
                                    FROM Cursos AS C 
                                    INNER JOIN Areas AS A ON C.AreaId = A.Id";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    cmd.CommandType=CommandType.Text;
                    // Lê todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Usando o laço while 
                        while (reader.Read() && reader != null)
                        {
                            cursos.Add(new Cursos
                            {
                                Id = (int)reader["Id_Cursos"],
                                Nome = reader["Nome_Cursos"].ToString(),
                                CargaHoraria = (int)reader["Hora_Cursos"],
                                Area = new Areas
                                {
                                    Id = (int)reader["Id_Area_Cursos"],
                                    Area = reader[5].ToString(),
                                    Imagem = reader[6].ToString()
                                },
                                Imagem = reader["Imagem_Cursos"].ToString()   
                            });
                        }
                    }
                }

            }

            return cursos;
        }

        //Busca pelo Id
        public Cursos GetById(int id)
        {
            Cursos cursos = null; // Objeto criado atraves da classe Cursos

            using (SqlConnection conexao = new SqlConnection(connectionCall))
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
                                Area = new Areas
                                {
                                    Id = (int)result[3],
                                    Area = null,
                                    Imagem = null
                                },
                            Imagem = (string)result[4]
                            };

                        }
                    }
                }

            }
            return cursos; // retorna o cursos selecionado através do Id
        }

        //Cadastra um curso
        public Cursos Insert(Cursos cursos)
        {
            using (SqlConnection conexao = new SqlConnection(connectionCall)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open();   // Abre uma conexao

                // escrever a consulta de inserção
                string script = "INSERT INTO Cursos (Nome, CargaHoraria, AreaId, Imagem) VALUES (@Nome, @CargaHoraria, @AreaId, @Imagem)";

                // O comando de execução no banco é criado
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //As declarações das variaveis por parametros são feitas
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = cursos.Nome;
                    cmd.Parameters.Add("@CargaHoraria", SqlDbType.Int).Value = cursos.CargaHoraria;
                    cmd.Parameters.Add("@AreaId", SqlDbType.Int).Value = cursos?.Area?.Id ?? 0;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = cursos.Imagem;

                    cmd.CommandType = CommandType.Text; // Tipo de comando Enum, do tipo texto.
                    cmd.ExecuteNonQuery(); //returna o número de linhas afetadas
                }
            }

            return cursos; //retorna a area criada
        }

        //Altera o nome e imagem da classe
        public Cursos Update(int id, Cursos cursos)
        {
            using (SqlConnection conexao = new SqlConnection(connectionCall)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); //conexão iniciada

                // escrever a consulta de atualização dos dados
                string script = "UPDATE Cursos SET Nome=@Nome, Imagem=@Imagem WHERE Id=@id";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //Declarações das variáveis por parametro
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = cursos.Id;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = cursos.Nome;
                    cmd.Parameters.Add("@Imagem", SqlDbType.NVarChar).Value = cursos.Imagem;

                    // Tipo de comando, tipo texto
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return cursos; //campo alterado 
        }
    }
}
