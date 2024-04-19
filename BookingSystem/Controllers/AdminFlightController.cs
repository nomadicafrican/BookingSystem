using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    public class AdminFlightController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminFlightController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminFlight
        public async Task<IActionResult> Index()
        {
            var flights = await _context.Flights.ToListAsync();
            List<AdminFlightViewModel> flightList = new List<AdminFlightViewModel>();

            if (flights != null)
            {
                foreach (var flight in flights)
                {
                    var adminFlightViewModel = new AdminFlightViewModel()
                    {
                        Id = flight.Id,
                        FlightNumber = flight.FlightNumber,
                        DepartureAirport = flight.DepartureAirport,
                        ArrivalAirport = flight.ArrivalAirport
                    };
                    flightList.Add(adminFlightViewModel);
                }
            }

            return View(flightList);
        }

        // GET: AdminFlight/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminFlight/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FlightNumber,DepartureAirport,ArrivalAirport")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();

               
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: AdminFlight/Edit/5
        [HttpGet]
        [Route("AdminFlight/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: AdminFlight/Edit/5
        [HttpPost]
        [Route("AdminFlight/Edit/{id}")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightNumber,DepartureAirport,ArrivalAirport")] Flight flight)
        {
            if (id != flight.Id)
            {
                Console.WriteLine("NotF:");
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
               
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                // Return the view with validation errors
                return View(flight);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing flight record from the database
                    var existingFlight = await _context.Flights.FindAsync(id);

                    if (existingFlight == null)
                    {
                       
                        return NotFound(); // If the flight doesn't exist, return NotFound
                    }

                    // Update the properties of the existing flight record
                    existingFlight.FlightNumber = flight.FlightNumber;
                    existingFlight.DepartureAirport = flight.DepartureAirport;
                    existingFlight.ArrivalAirport = flight.ArrivalAirport;
                    // Log the existing flight details before updating
                    if (_context.Entry(existingFlight).State != EntityState.Modified)
                    {
                        Console.WriteLine("Entity is not being tracked by EF Core.");
                    }
                    Console.WriteLine("Existing Flight Details:");
                    Console.WriteLine($"Flight Number: {existingFlight.FlightNumber}");
                    Console.WriteLine($"Departure Airport: {existingFlight.DepartureAirport}");
                    Console.WriteLine($"Arrival Airport: {existingFlight.ArrivalAirport}");

                    // Save changes to the database
                     _context.Flights.Update(existingFlight); // Update method is used for existing entities
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action after successful update
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Log the exception or handle it as needed
                    // For concurrency conflicts, you may want to reload the entity and retry the update
                    return HandleConcurrencyException(ex);
                }
           
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    Console.WriteLine("Error updating flight: " + ex.Message);
                    return HandleGeneralException(ex);
                }
            }
            return View(flight);
        }

        private IActionResult HandleConcurrencyException(DbUpdateConcurrencyException ex)
        {
            // Reload the entity from the database and retry the update
            ex.Entries.Single().Reload();
            return RedirectToAction("Index");
        }

        private IActionResult HandleGeneralException(Exception ex)
        {
            // Log the exception and return an appropriate error message
            // You may also redirect to an error page
            return RedirectToAction("Error");
        }

        // GET: AdminFlight/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: AdminFlight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            // Redirect to the Index action after successful deletion
            return RedirectToAction("Index");
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }
    }
}
