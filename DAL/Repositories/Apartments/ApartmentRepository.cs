using BookingApi.Data;
using BookingApi.Models;

namespace DAL.Repositories.Accommodations
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly DataContext _context;
        public ApartmentRepository(DataContext context)
        {
            _context = context;
        }

        public bool Add(Apartment accommodation)
        {
            _context.Add(accommodation);
            return Save();
        }

        public bool Remove(Apartment accommodation)
        {
            _context.Remove(accommodation);
            return Save();
        }

        public Apartment GetAccommodationById(int id)
        {
            return _context.Apartments.Where(a => a.Id == id).FirstOrDefault();
        }

        //public IEnumerable<Apartment> GetAccomodationsByReservation(int reservationId)
        //{
        //    return _context.Apartments.Where(r => r. == reservationId).Select(a => a.Accommodation).ToList();
        //}

        public IEnumerable<Apartment> GetAll()
        {
            return _context.Apartments.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Apartment accommodation)
        {
            _context.Update(accommodation);
            return Save();
        }
    }
}
