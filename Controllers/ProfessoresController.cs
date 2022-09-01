using APICursosGratuitos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessoresController : ControllerBase
    {
        private ProfessoresRepository repositorio = new ProfessoresRepository();
       
    }
}
