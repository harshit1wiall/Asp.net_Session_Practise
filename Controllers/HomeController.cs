using Microsoft.AspNetCore.Mvc;


namespace LoginPage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                if (HttpContext.Session.GetString("User").Any())
                {
                    //  Console.WriteLine("insde tempdata=--->" + TempData["name"]);
                    ViewBag.name = TempData["name"];
                    return View();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");

        }
        public IActionResult Logout()
        {
            Console.WriteLine("I am in Logout");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        public IActionResult Entry()
        {
            Console.WriteLine("into enrty field");
            return RedirectToAction("Index", "Data");
        }
    }
}
