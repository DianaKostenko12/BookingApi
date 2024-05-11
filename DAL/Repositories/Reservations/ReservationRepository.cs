using BookingApi.Data;
using BookingApi.Models;

namespace DAL.Repositories.Reservations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;
        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            return Save();
        }

        public bool DeleteReservation(Reservation reservation)
        {
            _context.Remove(reservation);
            return Save();
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations.OrderBy(r => r.Id).ToList();
        } 

        public Reservation GetReservationById(int id)
        {
            return _context.Reservations.Where(r => r.Id == id).FirstOrDefault();
        }

        //public ICollection<Reservation> GetReservationsByAccommodation(int accommodationId)
        //{
        //    return _context.AccommodationsReservations.Where(a => a.AccommodationId == accommodationId).Select(r => r.Reservation).ToList();
        //}

        public ICollection<Reservation> GetReservationsByUser(int userId)
        {
            return _context.Reservations.Where(u => u.User.Id == userId).ToList();
        }
        
        public bool ReservationExists(int reservationId)
        {
            return _context.Reservations.Any(u => u.Id == reservationId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReservation(Reservation reservation)
        {
            _context.Update(reservation);
            return Save();
        }
    }
}
