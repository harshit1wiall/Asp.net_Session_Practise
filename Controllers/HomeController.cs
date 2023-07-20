using Microsoft.AspNetCore.Mvc;

namespace LoginPage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
