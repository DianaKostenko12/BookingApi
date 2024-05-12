using BLL.Services.Reservations.Descriptors;
using BookingApi.Models;
using ErrorOr;

namespace BLL.Services.Reservations
{
    public interface IReservationService
    {
        ErrorOr<Success> BookReservation(CreateReservationDescriptor descriptor);
        ErrorOr<Success> UnbookReservation(int reservationId);
        List<Reservation> GetReservationsByUser(string email);
    }
}
