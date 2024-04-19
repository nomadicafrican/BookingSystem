using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using System.Linq;

namespace BookingSystem.Controllers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hotels = _context.Hotels.ToList();
            return View(hotels);
        }

       
    }
}
