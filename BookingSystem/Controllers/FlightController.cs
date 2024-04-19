using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using BookingSystem.Models;

namespace BookingSystem.Controllers
{
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var flights = _context.Flights.ToList();
                return View(flights);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving flights: {ex.Message}");
                return View(Enumerable.Empty<Flight>());
            }
        }
    }
}
