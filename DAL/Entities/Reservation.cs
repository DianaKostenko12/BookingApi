namespace BookingApi.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
    }
}
