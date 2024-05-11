using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApi.Models;


namespace DAL.Repositories.AccommodationReservations
{
    public interface IAccommodationReservationRepository
    {
        void CreateRange(IEnumerable<AccommodationReservation> accommodationReservations);
        bool Save();
    }
}
