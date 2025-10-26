using Microsoft.AspNetCore.Mvc;

namespace lab_5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Error()
        {
            return View(); // uses Views/Shared/Error.cshtml
        }
    }
}
