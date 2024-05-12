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

        public bool Add(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            return Save();
        }

        public bool Delete(Reservation reservation)
        {
            _context.Remove(reservation);
            return Save();
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations.OrderBy(r => r.Id).ToList();
        } 

        public Reservation GetById(int id)
        {
            return _context.Reservations.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Reservation> GetByUserId(int userId)
        {
            return _context.Reservations.Where(u => u.User.Id == userId).ToList();
        }
        
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Reservation reservation)
        {
            _context.Update(reservation);
            return Save();
        }
    }
}
