using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APICursosGratuitos.Repositories
{
    // REPOSITORY PATTERN ---> Padrão de repositórios que visa desacoplar a interação com o BD.

    public class AlunosRepository : IAlunosRepository
    {
        // Criar string de conexão com o Banco de Dados
        //variável de apenas leitura = readonly
        readonly string connection = "Data Source=WORKSTATIONSOUZ\\SQLEXPRESS;Integrated Security=true;Initial Catalog=CursosGratuitos";


        //Deletar aluno através do RA - DELETE
        public bool Delete(int Ra)
        {
            using (SqlConnection conexao = new SqlConnection(connection)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); // abrir conexão

                // escreve a consulta de exclusão
                string script = "DELETE FROM Alunos WHERE RA=@ra";

                // Comando de execução no banco criado
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //Declarações das variaveis por parametros
                    cmd.Parameters.Add("@ra", SqlDbType.Int).Value = Ra;

                    // Tipo de comando, tipo texto. CommandType é um Enum
                    cmd.CommandType = CommandType.Text;
                    int linhasAfetadas = cmd.ExecuteNonQuery(); //Retorna os numeros (RA) das linhas alteradas/afetadas 
                    if (linhasAfetadas == 0)
                    {
                        return false; // não retorna nada
                    }
                } 

            }

            return true;
        }


        // Buscar tudo - GET
        public ICollection<Alunos> GetAll()
        {
            var alunos = new List<Alunos>(); // Criando um objeto do tipo lista para exibir todas as colunas e dados da tabela Alunos

            using (SqlConnection conexao = new SqlConnection(connection))
            {
                conexao.Open();

                // Escreve a consulta/script de busca
                string consulta = "SELECT * FROM Alunos";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {
                    // Ler todos os itens da consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Colunas lidas através de um laço condicional
                        while (reader.Read())
                        {
                            alunos.Add(new Alunos
                            {
                                // Cada coluna, criada pelo Model, representa uma coluna da tabela Alunos, contendo seus respectivos tipo de dado e posição de índice da array
                                Ra = (int)reader[0],
                                Usuario = (string)reader[1],
                                Nome = (string)reader[2],
                                Cpf = (string)reader[3],
                                Email = (string)reader[4],
                                Senha = (string)reader[5]                               
                            });
                        }
                    }
                }

            }

             return alunos; // retorna a lista alunos
        }


        // Buscar por Id/RA - GET
        public Alunos GetById(int ra)
        {
            Alunos alunos = null; // Objeto criado atraves da classe Alunos

            using (SqlConnection conexao = new SqlConnection(connection))
            {
                conexao.Open(); // abre a conexão

                // Escreve a script de busca por RA
                string consulta = "SELECT * FROM Alunos WHERE RA=@ra";

                using (SqlCommand cmd = new SqlCommand(consulta, conexao))
                {

                    cmd.Parameters.Add("@ra", SqlDbType.Int).Value = ra;

                    // Ler todos os itens da consulta
                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            alunos = new Alunos
                            {
                                Ra = (int)result[0],
                                Usuario = (string)result[1],
                                Nome = (string)result[2],
                                Cpf = (string)result[3],
                                Email = (string)result[4],
                                Senha = (string)result[5]


                            };

                        }
                    }
                }

            }
            return alunos; // retorna o aluno do RA buscado
        }


        //Cadastra os dados do aluno - INSERT 
        public Alunos Insert(Alunos alunos)
        {
          
            // parametro que gerencia certos comandos de conexão
            using (SqlConnection conexao = new SqlConnection(connection)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open();   // Abre uma conexao

                // escrever a consulta de inserção
                string script = "INSERT INTO Alunos (Usuario, Nome, CPF, Email, Senha) VALUES (@Usuario, @Nome, @Cpf, @Email, @Senha)";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazemos as declarações das variaveis por parametros
                    cmd.Parameters.Add("@Usuario", SqlDbType.NVarChar).Value = alunos.Usuario;
                    cmd.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = alunos.Nome;
                    cmd.Parameters.Add("@Cpf", SqlDbType.NVarChar).Value = alunos.Cpf;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = alunos.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = alunos.Senha;
                                     
                    cmd.CommandType = CommandType.Text; // Tipo de comando do tipo texto. CommandType é um Enum
                    cmd.ExecuteNonQuery(); //returna o número de linhas afetadas/alteradas
                }

            }

            return alunos;
        }


        // Edita/Altera algum dado do aluno - UPDATE
        public Alunos Update(int ra, Alunos alunos)
        {
            using (SqlConnection conexao = new SqlConnection(connection)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); //conexão iniciada

                // escrever a consulta de atualização de dados
                string script = "UPDATE Alunos SET Usuario=@Usuario, Nome=@Nome, CPF=@Cpf, Email=@Email, Senha=@Senha WHERE RA=@ra";

                // Criamos o comando de execução no banco
                using (SqlCommand cmd = new SqlCommand(script, conexao))
                {
                    //fazemos as declarações das variaveis por parametros

                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = alunos.Email;
                    cmd.Parameters.Add("@Senha", SqlDbType.NVarChar).Value = alunos.Senha;

                    // Tipo de comando, tipo texto. CommandType é um Enum
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    alunos.Ra = ra;
                }

            }

            return alunos; // o atributo alterado, do objeto alunos 
        }


        // Interface com função 'Delete()' implementada
        Alunos IAlunosRepository.Delete(int Ra)
        {
            throw new System.NotImplementedException();
        }
    }
}
