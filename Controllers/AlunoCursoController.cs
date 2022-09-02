using Microsoft.AspNetCore.Mvc;

namespace APICursosGratuitos.Controllers
{
    public class AlunoCursoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
