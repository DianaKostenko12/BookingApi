namespace BookingApi.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ICollection<AccommodationReservation> AccommodationReservations { get; set; }
    }
}
