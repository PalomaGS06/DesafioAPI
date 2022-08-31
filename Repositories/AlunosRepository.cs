﻿using APICursosGratuitos.Interfaces;
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

                // escreve a consulta 
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
            var aluno = new Alunos(); // Objeto criado atraves da classe Alunos

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

                            aluno.Ra = (int)result[0];
                            aluno.Usuario = (string)result[1];
                            aluno.Nome = (string)result[2];
                            aluno.Cpf = (string)result[3];
                            aluno.Email = (string)result[4];
                            aluno.Senha = (string)result[5];

                        }
                    }
                }

            }
            return aluno; // retorna o aluno do RA buscado
        }

        //Cadastra os dados do aluno - INSERT 
        public Alunos Insert(Alunos alunos)
        {
            throw new System.NotImplementedException();
        }

        // Edita/Altera algum dado do aluno - UPDATE
        public Alunos Update(int id, Alunos alunos)
        {
            throw new System.NotImplementedException();
        }


        // Interface com função 'Delete()' implementada
        Alunos IAlunosRepository.Delete(int Ra)
        {
            throw new System.NotImplementedException();
        }
    }
}
