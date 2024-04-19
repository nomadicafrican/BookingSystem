namespace BookingSystem.Models
{
    public class FlightViewModel
    {

        public FlightViewModel()
        {
            FlightNumber = string.Empty;
            DepartureAirport = string.Empty;
            ArrivalAirport = string.Empty;
        }
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
    }
}
