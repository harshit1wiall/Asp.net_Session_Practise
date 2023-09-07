using LoginPage.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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
                    DbMethods2 dbMethods = new DbMethods2();
                    var result = dbMethods.ShowAll().Result;
                    Console.WriteLine(result);
                    ViewBag.name = TempData["name"];
                    return View(result);
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
            // Console.WriteLine("I am in Logout");
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
