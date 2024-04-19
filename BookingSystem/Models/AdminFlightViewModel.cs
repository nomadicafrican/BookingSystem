using System.ComponentModel;

namespace BookingSystem.Models
{
    public class AdminFlightViewModel
    {
        public AdminFlightViewModel()
        {
            FlightNumber = string.Empty;
            DepartureAirport = string.Empty;
            ArrivalAirport = string.Empty;
        }
        public int Id { get; set; }
        [DisplayName("FlightNumber")]
        public string FlightNumber { get; set; }
        [DisplayName("DepartureAirport")]
        public string DepartureAirport { get; set; }
        [DisplayName("ArrivalAirport")]
        public string ArrivalAirport { get; set; }
    }
}
