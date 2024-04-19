namespace BookingSystem.Models
{
    public class Hotel
    {
        public Hotel()
        {
            Name = string.Empty;
            Location = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PricePerNight { get; set; }
        public int Rating { get; set; }
    }

}
