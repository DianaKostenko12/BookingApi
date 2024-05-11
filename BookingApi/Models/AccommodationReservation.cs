namespace BookingApi.Models
{
    public class AccommodationReservation
    {
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
