namespace BookingSystem.Models
{
    public class CarRental
    {
        public CarRental()
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
