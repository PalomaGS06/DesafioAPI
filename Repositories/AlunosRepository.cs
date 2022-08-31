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

        public bool Delete(int Ra)
        {
            using (SqlConnection conexao = new SqlConnection(connection)) // dentro do parametro se passa a string de conexao
            {
                conexao.Open(); // abrir conexão

                // escrever a consulta 
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

        public ICollection<Alunos> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Alunos GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Alunos Insert(Alunos alunos)
        {
            throw new System.NotImplementedException();
        }

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
