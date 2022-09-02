using Microsoft.AspNetCore.Mvc;

namespace APICursosGratuitos.Controllers
{
    public class CursosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
