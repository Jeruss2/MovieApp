using Microsoft.AspNetCore.Mvc;

namespace RentalServicesWebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult CreateAccount()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
    }
}
