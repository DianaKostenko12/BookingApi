using BookingApi.Models;

namespace DAL.Repositories.Reservations
{
    public interface IReservationRepository
    {
        ICollection<Reservation> GetByUserEmail(string email);
        bool IsReservationBusy(int apartmentId, DateTime start, DateTime end);
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        void Add(Reservation reservation);
        void Delete(Reservation reservation);
        bool Save();
    }
}
