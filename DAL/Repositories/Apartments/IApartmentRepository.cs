using BookingApi.Models;

namespace DAL.Repositories.Accommodations
{
    public interface IApartmentRepository
    {
        IEnumerable<Apartment> GetAll();
        Apartment GetAccommodationById(int id);
        bool Add(Apartment accommodation);
        bool Update(Apartment accommodation);
        bool Remove(Apartment accommodation);
        bool Save();
        //IEnumerable<Apartment> GetAccomodationsByReservation(int reservationId);
    }
}
