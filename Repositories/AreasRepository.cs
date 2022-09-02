using APICursosGratuitos.Interfaces;
using Microsoft.Extensions.Configuration;

namespace APICursosGratuitos.Repositories
{
    public class AreasRepository : IAreasRepository
    {
        public AreasRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("CursosGratuitos"); // String de conexão chamado através do arquivo appsettings.json
        }

        public IConfiguration Configuration { get; set; }
        private string connectionString { get; set; }



    }
}
