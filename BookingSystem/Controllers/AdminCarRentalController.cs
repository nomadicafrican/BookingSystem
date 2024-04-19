using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    public class AdminCarRentalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminCarRentalController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool CarRentalExists(int id)
        {
            return _context.CarRentals.Any(e => e.Id == id);
        }

        // GET: AdminCarRental
        public async Task<IActionResult> Index()
        {
            var carRentals = await _context.CarRentals.ToListAsync();
            List<AdminCarRentalViewModel> carRentalList = new List<AdminCarRentalViewModel>();

            if (carRentals != null)
            {
                foreach (var carRental in carRentals)
                {
                    var adminCarRentalViewModel = new AdminCarRentalViewModel()
                    {
                        Id = carRental.Id,
                        Model = carRental.Model,
                        RentalCompany = carRental.RentalCompany,
                        PricePerDay = carRental.PricePerDay
                    };
                    carRentalList.Add(adminCarRentalViewModel);
                }
            }

            return View(carRentalList);
        }

        // GET: AdminCarRental/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminCarRental/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,RentalCompany,PricePerDay")] CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carRental);
                await _context.SaveChangesAsync();

                // Redirect to the Index action after successful creation
                return RedirectToAction("Index");
            }
            return View(carRental);
        }

        // GET: AdminCarRental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _context.CarRentals.FindAsync(id);
            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }

        // POST: AdminCarRental/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,RentalCompany,PricePerDay")] CarRental carRental)
        {
            if (id != carRental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing car rental record from the database
                    var existingCarRental = await _context.CarRentals.FindAsync(id);

                    // Update the properties of the existing car rental record
                    existingCarRental.Model = carRental.Model;
                    existingCarRental.RentalCompany = carRental.RentalCompany;
                    existingCarRental.PricePerDay = carRental.PricePerDay;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action after successful update
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarRentalExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(carRental);
        }

        // GET: AdminCarRental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _context.CarRentals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }

        // POST: AdminCarRental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carRental = await _context.CarRentals.FindAsync(id);
            _context.CarRentals.Remove(carRental);
            await _context.SaveChangesAsync();

            // Redirect to the Index action after successful deletion
            return RedirectToAction("Index");
        }
    }
}
