using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;

namespace BookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking/Index
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.ToListAsync();
            List<AdminBookingModelView> bookingList = new List<AdminBookingModelView>();

            if (bookings != null)
            {
                foreach (var booking in bookings)
                {
                    var adminBookingModelView = new AdminBookingModelView()
                    {
                        Id = booking.Id,
                        UserId = booking.UserId,
                        FlightId = booking.FlightId,
                        HotelId = booking.HotelId,
                        CarRentalId = booking.CarRentalId,
                        BookingDateTime = booking.BookingDateTime
                    };
                    bookingList.Add(adminBookingModelView);
                }
            }

            return View(bookingList);
        }

        // POST: Booking/Book
        [HttpPost]
        public async Task<IActionResult> Book(int? hotelId, int? flightId, int? carRentalId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            if (hotelId != null)
            {
                // Booking for a hotel
                var booking = new Booking
                {
                    UserId = userId,
                    HotelId = hotelId.Value,
                    BookingDateTime = DateTime.Now  // Set the current DateTime
                };

                _context.Bookings.Add(booking);
            }
            else if (flightId != null)
            {
                // Booking for a flight
                var booking = new Booking
                {
                    UserId = userId,
                    FlightId = flightId.Value,
                    BookingDateTime = DateTime.Now  // Set the current DateTime
                };

                _context.Bookings.Add(booking);
            }
            else if (carRentalId != null)
            {
                // Booking for a car rental
                var booking = new Booking
                {
                    UserId = userId,
                    CarRentalId = carRentalId.Value,
                    BookingDateTime = DateTime.Now  // Set the current DateTime
                };

                _context.Bookings.Add(booking);
            }

            await _context.SaveChangesAsync();

            // Redirect to the Index action after successful booking
            return RedirectToAction("Index", "Booking");
        }


        // POST: Booking/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
