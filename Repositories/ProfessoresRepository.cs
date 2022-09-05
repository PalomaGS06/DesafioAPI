using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APICursosGratuitos.Repositories
{
    public class ProfessoresRepository : IProfessoresRepository
    {
        public ProfessoresRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionn = Configuration.GetConnectionString("CursosGratuitos"); // String de conexão chamado através do arquivo appsettings.json
        }

        public IConfiguration Configuration { get; set; }
        private string connectionn { get; set; }


        // Cria uma string de conexão com o Banco de Dados
        //variável de apenas leitura = readonly


        //Deletar o professor através do CPF - DELETE
        public bool Delete(int Cpf)
        {          
                using (SqlConnection conexao = new SqlConnection(connectionn)) // dentro do parametro se passa a string de conexao
                {
                    conexao.Open(); // abrir conexão

                    // escreve a consulta de exclusão
                    string script = "DELETE FROM Professores WHERE CPF=@Cpf";

                    // Comando de execução no banco criado
                    using (SqlCommand cmd = new SqlCommand(script, conexao))
                    {
                        //Declarações das variaveis por parametros
                        cmd.Parameters.Add("@Cpf", SqlDbType.Int).Value = Cpf;

                        // Tipo de comando, tipo texto. CommandType é um Enum
                        cmd.CommandType = CommandType.Text;
                        int linhasAfetadas = cmd.ExecuteNonQuery(); //Retorna os numeros (CPF) das linhas alteradas/afetadas 
                        if (linhasAfetadas == 0)
                        {
                            return false; // não retorna nada
                        }
                    }

                }

              return true;            
        }

        //Buscar tudo - GET
        public ICollection<Professores> GetAll()
        {
            var professores = new List<Professores>(); // Criando um objeto do tipo lista para exibir todas as colunas e dados da tabela Professores

            using (SqlConnection conexao = new SqlConnection(connectionn))
            {
                conexao.Open();

                // Escreve a consulta/script de busca
                string consulta = "SELECT * FROM Professores";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Colunas lidas através de um laço condicional
                        while (reader.Read())
                        {
                            professores.Add(new Professores
                            {
                                // Cada coluna, criada pelo Model, representa uma coluna da tabela Professores, contendo seus respectivos tipo de dado e posição de índice da array
                                Cpf = (int)reader[0],
                                Nome = reader[1].ToString(),
                                Email = reader[2].ToString()
                            });
                        }
                    }
                }

            }

            return professores; // retorna a lista professores
        }

        //Buscar por CPF - GET
        public Professores GetById(int Cpf)
        {
            Professores profs = null; // Objeto criado atraves da classe Professores

            using (SqlConnection conexao = new SqlConnection(connectionn))
            {
                conexao.Open(); // abre a conexão

                // Escreve a script de busca por CPF
                string consulta = "SELECT * FROM Professores WHERE CPF=@Cpf";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {

                    cmd.Parameters.Add("@Cpf", SqlDbType.Int).Value = Cpf;

                    // Ler todos os itens da consulta
                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            profs = new Professores
                            {
                                Cpf = (int)result[0],                 
                                Nome = result[1].ToString(),                               
                                Email = result[2].ToString()

                            };

                        }
                    }
                }

            }
            return profs; // retorna o professor do CPF selecionado
        }

        //Cadastra os dados do professor - INSERT
        public Professores Insert(Professores profs)
        {
            using (SqlConnection conexao = new SqlConnection(connectionn)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open();   // Abre uma conexao

                // escrever a consulta de inserção
                string script = "INSERT INTO Professores (Nome, Email) VALUES (@Nome, @Email)";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazemos as declarações das variaveis por parametros
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = profs.Nome;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = profs.Email;

                    cmd.CommandType = CommandType.Text; // Tipo de comando Enum, do tipo texto.
                    cmd.ExecuteNonQuery(); //returna o número de linhas afetadas
                }

            }

            return profs;
        }

        //Altera algum dado do professor - UPDATE
        public Professores Update(int Cpf, Professores profs)
        {
            using (SqlConnection conexao = new SqlConnection(connectionn)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); //conexão iniciada

                // escrever a consulta de atualização do email
                string script = "UPDATE Professores SET Email=@Email WHERE CPF=@Cpf";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    cmd.Parameters.Add("@Cpf", SqlDbType.Int).Value = Cpf;
                    //Declarações da variável Email por parametro
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = profs.Email;

                    // Tipo de comando, tipo texto
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }

            return profs; //atributo alterado, do parâmetro profs 
        }

        Professores IProfessoresRepository.Delete(int Cpf)
        {
            throw new System.NotImplementedException();
        }
    }

}
