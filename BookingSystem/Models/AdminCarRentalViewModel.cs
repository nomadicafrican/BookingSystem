using System.ComponentModel;

namespace BookingSystem.Models
{
    public class AdminCarRentalViewModel
    {
        public AdminCarRentalViewModel()
        {
            Model = string.Empty;
            RentalCompany = string.Empty;
        }

        public int Id { get; set; }
        [DisplayName("Model")]
        public string Model { get; set; }
        [DisplayName("RentalCompany")]
        public string RentalCompany { get; set; }
        [DisplayName("PricePerDay")]
        public decimal PricePerDay { get; set; }
    }
}
