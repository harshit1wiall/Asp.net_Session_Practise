using LoginPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoginPage.Controllers
{
    public class LoginController : Controller
    {


        public IActionResult Index()
        {

            return View();
        }







        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            //Console.WriteLine("Top-->" + email + " " + password);
            if (email != null && password != null)
            {
                //Console.WriteLine("In if condition--> " + email + " " + password);
                try
                {
                    DbMethods db = new DbMethods();


                    var result = await db.FindUserByEmail(email, password);
                    if (result)
                    {
                        ViewBag.status = true;
                        return RedirectToAction("Index", "Home");


                    }
                    else
                    {
                        ViewBag.status = false;
                        ViewBag.message = "User not found";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return View();
        }


    }


}