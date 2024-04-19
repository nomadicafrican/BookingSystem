using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using System.Linq;

namespace BookingSystem.Controllers
{
    public class CarRentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarRentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var carRentals = _context.CarRentals.ToList();
            return View(carRentals);
        }

        
    }
}
