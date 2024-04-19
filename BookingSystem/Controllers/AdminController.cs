using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Flight()
        {
            // Logic for handling flight functionality
            return View();
        }

        public IActionResult Hotel()
        {
            // Logic for handling hotel functionality
            return View();
        }

        public IActionResult CarRental()
        {
            // Logic for handling car rental functionality
            return View();
        }
    }
}
