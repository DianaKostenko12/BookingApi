using BookingApi.Models;

namespace DAL.Repositories.Accommodations
{
    public interface IApartmentRepository
    {
        IEnumerable<Apartment> GetAll();
        Apartment GetById(int id);
        bool Add(Apartment apartment);
        bool Update(Apartment apartment);
        bool Remove(Apartment apartment);
        bool Save();
    }
}
