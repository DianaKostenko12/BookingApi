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

        // Method to book a reservation
        public ErrorOr<Success> BookReservation(CreateReservationDescriptor descriptor)
        {
            // Get the apartment by ID
            Apartment apartment = _apartmentRepository.GetById(descriptor.RoomId);
            if (apartment == null)
            {
                return Error.NotFound(description: "Apartment is not found");
            }

            // Get the user by email
            User user = _userRepository.GetUserByEmail(descriptor.Email);

            // Check if the apartment is already reserved for the specified dates
            if (_reservationRepository.IsReservationBusy(descriptor.RoomId, descriptor.CheckInDate, descriptor.CheckOutDate))
            {
                return Error.Failure(description: "Time collision");
            }

            // Create a new reservation
            var reservation = new Reservation()
            {
                CheckInDate = descriptor.CheckInDate,
                CheckOutDate = descriptor.CheckOutDate,
                User = user,
                Apartment = apartment
            };

            // Add new reservation to the database
            _reservationRepository.Add(reservation);
            _reservationRepository.Save();

            return Result.Success;
        }

        // Method to unbook a reservation
        public ErrorOr<Success> UnbookReservation(int reservationId)
        {
            // Get the reservation by ID
            Reservation reservation = _reservationRepository.GetById(reservationId);
            if (reservation == null)
            {
                return Error.NotFound(description: "Reservation is not found");
            }

            // Delete the reservation from the repository
            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();

            return Result.Success;
        }

        // Method to get reservations for a user
        public List<Reservation> GetReservationsByUser(string email)
        {
            // Get reservations for the user by email
            return _reservationRepository.GetByUserEmail(email).ToList();
        }
    }
}
