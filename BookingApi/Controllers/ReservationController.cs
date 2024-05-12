using BookingApi.DTOs;
using BookingApi.Models;
using DAL.Repositories.Accommodations;
using DAL.Repositories.Reservations;
using DAL.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;
        public ReservationController(IReservationRepository reservationRepository, IUserRepository userRepository, IApartmentRepository apartmentRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
        }
        [HttpPost]
        public IActionResult BookReservation(CreateReservationRequest request)
        {
            Apartment apartment = _apartmentRepository.GetById(request.RoomId);
            if (apartment == null)
            {
                return BadRequest("Apartment is not found.");
            }

            string email = User.FindFirstValue(ClaimTypes.Email);
            User user = _userRepository.GetUserByEmail(email);

            var reservation = new Reservation() 
            {
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                User = user,
                Apartment = apartment
            };

            _reservationRepository.Add(reservation);
            _reservationRepository.Save();

            return Ok("Successfully created");
        }

        [HttpDelete]
        public IActionResult UnbookReservation(int id)
        {
            Reservation reservation = _reservationRepository.GetById(id);
            if (reservation == null)
            {
                return BadRequest("Reservation is not found");
            }

            _reservationRepository.Delete(reservation);
            _reservationRepository.Save();

            return NoContent();
        }

        //[HttpGet]
        //public IActionResult GetReservationsByUser(int userId) 
        //{

        //}
    }
}
