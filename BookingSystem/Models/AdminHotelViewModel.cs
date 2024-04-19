using System.ComponentModel;

namespace BookingSystem.Models
{
    public class AdminHotelViewModel
    {
        public AdminHotelViewModel()
        {
            Name = string.Empty;
            Location = string.Empty;
        }

        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Location")]
        public string Location { get; set; }
        [DisplayName("PricePerNight")]
        public decimal PricePerNight { get; set; }
        [DisplayName("Rating")]
        public int Rating { get; set; }
    }
}
