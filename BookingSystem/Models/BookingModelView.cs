namespace BookingSystem.Models
{
    public class BookingModelView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public int HotelId { get; set; }
        public int CarRentalId { get; set; }
        public DateTime BookingDateTime { get; set; }
    }
}
