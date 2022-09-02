using Microsoft.AspNetCore.Mvc;

namespace APICursosGratuitos.Controllers
{
    public class AulasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
