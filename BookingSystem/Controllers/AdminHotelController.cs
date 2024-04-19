using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    public class AdminHotelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminHotelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminHotel
        public async Task<IActionResult> Index()
        {
            var hotels = await _context.Hotels.ToListAsync();
            List<AdminHotelViewModel> hotelList = new List<AdminHotelViewModel>();

            if (hotels != null)
            {
                foreach (var hotel in hotels)
                {
                    var adminHotelViewModel = new AdminHotelViewModel()
                    {
                        Id = hotel.Id,
                        Name = hotel.Name,
                        Location = hotel.Location,
                        PricePerNight = hotel.PricePerNight,
                        Rating = hotel.Rating
                    };
                    hotelList.Add(adminHotelViewModel);
                }
            }

            return View(hotelList);
        }

        // GET: AdminHotel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminHotel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,PricePerNight,Rating")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();

                // Redirect to the Index action after successful creation
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: AdminHotel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        // POST: AdminHotel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,PricePerNight,Rating")] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing hotel record from the database
                    var existingHotel = await _context.Hotels.FindAsync(id);

                    // Update the properties of the existing hotel record
                    existingHotel.Name = hotel.Name;
                    existingHotel.Location = hotel.Location;
                    existingHotel.PricePerNight = hotel.PricePerNight;
                    existingHotel.Rating = hotel.Rating;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action after successful update
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(hotel);
        }

        // GET: AdminHotel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: AdminHotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            // Redirect to the Index action after successful deletion
            return RedirectToAction("Index");
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}
