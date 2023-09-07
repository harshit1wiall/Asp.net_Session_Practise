using LoginPage.Models;
using LoginPage.Service;
using Microsoft.AspNetCore.Mvc;

namespace LoginPage.Controllers
{
    public class DataController : Controller
    {
        private readonly DbMethods2 db = new DbMethods2();
        public IActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(Details data)
        {

            if (data != null)
            {
                await db.SaveUser(data);
            }

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Delete(string myId)
        {
            //Console.WriteLine("this is the id--->" + myId);
            if (!string.IsNullOrEmpty(myId))
            {
                await db.DeleteUser(myId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string myId)
        {
            if (!string.IsNullOrEmpty(myId))
            {
                // Retrieve the user data to be edited based on myId
                var userData = await db.GetUserByID(myId);

                if (userData != null && userData.Count > 0)
                {
                    // Assuming you want to edit a single user, take the first item in the list
                    var userToEdit = userData[0];

                    return View(userToEdit); // Return the user data to the edit view
                }
                else
                {
                    // Handle the case where the user with the specified myId is not found
                    return RedirectToAction("UserNotFound");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home"); // Handle the case where myId is empty
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Details updatedUserData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the user data in your database using the UpdateUser method
                    bool updateSuccess = await db.UpdateUser(updatedUserData.MyId, updatedUserData);

                    if (updateSuccess)
                    {
                        // If the update was successful, redirect to a success page or another action
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Handle the case where the update failed
                        ModelState.AddModelError("", "Failed to update user data.");
                        return View(updatedUserData);
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the update
                    ModelState.AddModelError("", "An error occurred while updating the user data.");
                    return View(updatedUserData); // Return to the edit view with an error message
                }
            }
            else
            {
                // If ModelState is not valid, return the edit view with validation errors
                return View(updatedUserData);
            }
        }
    }
}