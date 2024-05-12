namespace BLL.Services.Reservations.Descriptors
{
    public class CreateReservationDescriptor
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomId { get; set; }
        public string Email { get; set; }
    }
}
