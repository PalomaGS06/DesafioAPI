using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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

        public Areas Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Areas> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Areas GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Areas Insert(Areas areass)
        {
            throw new System.NotImplementedException();
        }

        public Areas Update(int id, Areas areas)
        {
            throw new System.NotImplementedException();
        }
    }
}
