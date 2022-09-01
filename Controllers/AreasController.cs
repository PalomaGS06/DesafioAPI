using APICursosGratuitos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APICursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private AreasRepository repositorio = new AreasRepository();
    }
}
