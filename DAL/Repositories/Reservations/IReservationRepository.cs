using BookingApi.Models;

namespace DAL.Repositories.Reservations
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        void Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(Reservation reservation);
        bool Save();
        ICollection<Reservation> GetByUserEmail(string email);
    }
}
