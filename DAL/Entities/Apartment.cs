namespace BookingApi.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal PricePerNight {  get; set; }
        public int NumberOfBeds {  get; set; }
        public string Description { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
