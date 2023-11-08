using Microsoft.AspNetCore.Mvc;

namespace App.Publisher.Api.Controllers
{
    public class AppMensagensController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
