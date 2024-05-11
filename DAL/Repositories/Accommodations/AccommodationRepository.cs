using BookingApi.Data;
using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Accommodations
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly DataContext _context;
        public AccommodationRepository(DataContext context)
        {
            _context = context;
        }

        public bool AccommodationExists(int accommodationId)
        {
            return _context.Accommodations.Any(u => u.Id == accommodationId);
        }

        public bool CreateAccommodation(Accommodation accommodation)
        {
            _context.Add(accommodation);
            return Save();
        }

        public bool DeleteAccommodation(Accommodation accommodation)
        {
            _context.Remove(accommodation);
            return Save();
        }

        public Accommodation GetAccommodationById(int id)
        {
            return _context.Accommodations.Where(a => a.Id == id).FirstOrDefault();
        }

        public IEnumerable<Accommodation> GetAccomodationsByReservation(int reservationId)
        {
            return _context.AccommodationsReservations.Where(r => r.ReservationId == reservationId).Select(a => a.Accommodation).ToList();
        }

        public IEnumerable<Accommodation> GetAllAccommodations()
        {
            return _context.Accommodations.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAccommodation(Accommodation accommodation)
        {
            _context.Update(accommodation);
            return Save();
        }
    }
}
