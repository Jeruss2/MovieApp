using Microsoft.AspNetCore.Mvc;
using MovieApp.Business;
using MovieApp.Models;

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
