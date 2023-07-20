using LoginPage.Models;
using LoginPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoginPage.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();

        }


        [HttpPost]

        public async Task<IActionResult> Index(People? user)
        {

            if (user != null)
            {
                DbMethods db = new DbMethods();
                var userExist = await db.FindUserByEmail(user.Username, user.Email, user.Password);
                if (userExist)
                {
                    ViewBag.success = false;
                    ViewBag.message = "User Already Exist";
                    return View();
                }
                else
                {
                    // Console.WriteLine("line 33 " + userExist);
                    ViewBag.success = true;
                    await db.SaveUser(user);

                    ViewBag.status = true;
                    ViewBag.message = "User Saved Successfully";
                    return RedirectToAction("Index", "Login");

                    //return View();

                }
            }
            return View();
        }
    }




}
