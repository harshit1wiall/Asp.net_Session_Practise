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

            string? name = user != null ? user.Name : "";
            Console.WriteLine(name);
            if (user != null)
            {
                DbMethods db = new DbMethods();
                var userExist = await db.FindUserByEmail(user.Email, user.Password);
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
/* if (password != repassword)
             {
                 ViewBag.result = "Password doesnt match";
             }
             else
             {
                 ViewBag.result = "Stored";
                 //  return RedirectToAction("Index", "Login");
                *//* var person = new MongoDBdemo() { Email = email, Password = password };
                 await collection.InsertOneAsync(person);*//*

                 //  var result = await collection.FindAsync(_ => true);
             }
            var person = new People() { Email = email, Password = password };
            await collection.InsertOneAsync(person);


            return View();
            */