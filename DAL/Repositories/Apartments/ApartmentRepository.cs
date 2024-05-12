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

        public bool Add(Apartment apartment)
        {
            _context.Add(apartment);
            return Save();
        }

        public bool Remove(Apartment apartment)
        {
            _context.Remove(apartment);
            return Save();
        }

        public Apartment GetById(int id)
        {
            return _context.Apartments.Where(a => a.Id == id).FirstOrDefault();
        }

        public IEnumerable<Apartment> GetAll()
        {
            return _context.Apartments.OrderBy(a => a.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Apartment apartment)
        {
            _context.Update(apartment);
            return Save();
        }
    }
}
