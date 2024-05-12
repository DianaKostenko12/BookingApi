namespace BookingApi.DTOs
{
    public class CreateReservationRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set;}
    }
}
