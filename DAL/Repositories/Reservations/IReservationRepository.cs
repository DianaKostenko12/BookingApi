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
        IEnumerable<Reservation> GetAllReservations();
        bool ReservationExists(int reservationId);
        Reservation GetReservationById(int id);
        bool CreateReservation(Reservation reservation);
        bool UpdateReservation(Reservation reservation);
        bool DeleteReservation(Reservation reservation);
        bool Save();
        ICollection<Reservation> GetReservationsByUser(int userId);
        ICollection<Reservation> GetReservationsByAccommodation(int accommodationId);
    }
}
