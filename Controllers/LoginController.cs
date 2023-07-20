using LoginPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoginPage.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {

            return View();
        }







        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            if (email != null && password != null)
            {
                var username = email;
                try
                {
                    DbMethods db = new DbMethods();


                    var result = await db.FindUserByEmail(username, email, password);
                    if (result)
                    {
                        HttpContext.Session.SetString("User", email);
                        Console.WriteLine(HttpContext.Session.GetString("User"));
                        if (HttpContext.Session.IsAvailable && HttpContext.Session.GetString("User").Any())
                        {
                            ViewBag.status = true;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.message = "Login Failed!!";
                        }
                    }

                    else
                    {
                        ViewBag.status = false;
                        ViewBag.message = "User not found";
                        return View();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return View(e);
                }
            }

            ViewBag.success = false;
            ViewBag.message = "Both Email and Password Required";

            return View();
        }


    }


}