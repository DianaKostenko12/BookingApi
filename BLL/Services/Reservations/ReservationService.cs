using BLL.Services.Reservations.Descriptors;
using BookingApi.Models;
using DAL.Repositories.Accommodations;
using DAL.Repositories.Reservations;
using DAL.Repositories.Users;
using ErrorOr;

namespace BLL.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;
        public ReservationService(
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            IApartmentRepository apartmentRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
        }

        public ErrorOr<Success> BookReservation(CreateReservationDescriptor descriptor)
        {
            Apartment apartment = _apartmentRepository.GetById(descriptor.RoomId);
            if (apartment == null)
            {
                return Error.NotFound(description: "Apartment is not found");
            }

            User user = _userRepository.GetUserByEmail(descriptor.Email);

            var reservation = new Reservation()
            {
                CheckInDate = descriptor.CheckInDate,
                CheckOutDate = descriptor.CheckOutDate,
                User = user,
                Apartment = apartment
            };

            _reservationRepository.Add(reservation);
            _reservationRepository.Save();

            return Result.Success;
        }

        public ErrorOr<Success> UnbookReservation(int reservationId)
        {
            Reservation reservation = _reservationRepository.GetById(reservationId);
            if (reservation == null)
            {
                return Error.NotFound(description: "Reservation is not found");
            }

            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();

            return Result.Success;
        }

        public List<Reservation> GetReservationsByUser(string email)
        {
            return _reservationRepository.GetByUserEmail(email).ToList();
        }
    }
}
