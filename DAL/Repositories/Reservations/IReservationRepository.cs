using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Reservations
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        bool Add(Reservation reservation);
        bool Update(Reservation reservation);
        bool Delete(Reservation reservation);
        bool Save();
        ICollection<Reservation> GetByUserId(int userId);
    }
}
