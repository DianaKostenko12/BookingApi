using BookingApi.Data;
using BookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Reservations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;
        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        public void Delete(Reservation reservation)
        {
            _context.Remove(reservation);
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
            return _context.Reservations
                .Include(r => r.Apartment)
                .Where(u => u.User.Id == userId)
                .ToList();
        }
        
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public void Update(Reservation reservation)
        {
            _context.Update(reservation);
        }
    }
}
