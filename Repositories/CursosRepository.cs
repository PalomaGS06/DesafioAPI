using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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
        //variável de apenas leitura = readonly

        readonly string connectionString = "Data Source=WORKSTATIONSOUZ\\SQLEXPRESS;Integrated Security=true;Initial Catalog=CursosGratuitos";

        //Deleta uma area através de seu Id
        public Cursos Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Cursos> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Cursos GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Cursos Insert(Cursos cursos)
        {
            throw new System.NotImplementedException();
        }

        public Cursos Update(int id, Cursos cursos)
        {
            throw new System.NotImplementedException();
        }
    }
}
