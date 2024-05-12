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

        public ICollection<Reservation> GetByUserEmail(string email)
        {
            return _context.Reservations
                .Include(r => r.Apartment)
                .Where(u => u.User.Email == email)
                .ToList();
        }

        public bool IsReservationBusy(int apartmentId, DateTime start, DateTime end)
        {
            return _context.Reservations
                .Where(r => r.ApartmentId == apartmentId)
                .Any(r => !(end <= r.CheckInDate || start >= r.CheckOutDate));
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations.OrderBy(r => r.Id).ToList();
        } 

        public Reservation GetById(int id)
        {
            return _context.Reservations.Where(r => r.Id == id).FirstOrDefault();
        }

        public void Add(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        public void Delete(Reservation reservation)
        {
            _context.Remove(reservation);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
