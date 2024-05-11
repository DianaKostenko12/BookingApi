using BookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Accommodations
{
    public interface IAccommodationRepository
    {
        IEnumerable<Accommodation> GetAllAccommodations();
        bool AccommodationExists(int accommodationId);
        Accommodation GetAccommodationById(int id);
        bool CreateAccommodation(Accommodation accommodation);
        bool UpdateAccommodation(Accommodation accommodation);
        bool DeleteAccommodation(Accommodation accommodation);
        bool Save();
        IEnumerable<Accommodation> GetAccomodationsByReservation(int reservationId);
    }
}
