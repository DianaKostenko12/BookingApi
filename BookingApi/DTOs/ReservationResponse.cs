namespace BookingApi.DTOs
{
    public class ReservationResponse
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string ApartmentName { get; set; }
    }
}
