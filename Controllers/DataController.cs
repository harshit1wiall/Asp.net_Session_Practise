using Microsoft.AspNetCore.Mvc;

namespace LoginPage.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(string username, string description)
        {

            Console.WriteLine(username);
            Console.WriteLine(description);

            return RedirectToAction("Index", "Home");

        }
    }
}
