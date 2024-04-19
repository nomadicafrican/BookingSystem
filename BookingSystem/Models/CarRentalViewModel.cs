namespace BookingSystem.Models
{
    public class CarRentalViewModel
    {
        public CarRentalViewModel()
        {
            Model = string.Empty;
            RentalCompany = string.Empty;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string RentalCompany { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
