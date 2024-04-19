using System.ComponentModel;

namespace BookingSystem.Models
{
    public class AdminBookingModelView
    {
        public int Id { get; set; }
        [DisplayName("UserId")]
        public int UserId { get; set; }
        [DisplayName("FlightId")]
        public int FlightId { get; set; }
        [DisplayName("HotelId")]
        public int HotelId { get; set; }
        [DisplayName("CarRentalId")]
        public int CarRentalId { get; set; }
        [DisplayName("BookingDateTime")]
        public DateTime BookingDateTime { get; set; }
    }
}
