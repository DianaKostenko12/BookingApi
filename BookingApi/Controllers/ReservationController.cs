using BookingApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using BLL.Services.Reservations.Descriptors;
using BLL.Services.Reservations;

namespace BookingApi.Controllers
{
    [ApiController, Route("reservations"), Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper; 
        public ReservationController(
            IReservationService reservationService,
            IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        // POST request to book a reservation
        [HttpPost]
        public IActionResult BookReservation(CreateReservationRequest request)
        {
            // Map CreateReservationRequest DTO to CreateReservationDescriptor using AutoMapper
            var descriptor = _mapper.Map<CreateReservationDescriptor>(request);
            // Set the email in the descriptor to the email of the authenticated user
            descriptor.Email = User.FindFirstValue(ClaimTypes.Email);

            var response = _reservationService.BookReservation(descriptor);

            return response.Match(
                response => Ok("Successfully booked"),
                error => Problem(statusCode: 400, detail: error.First().Description)
            );
        }

        // DELETE request to unbook a reservation
        [HttpDelete("{reservationId}")]
        public IActionResult UnbookReservation(int reservationId)
        {
            // Call UnbookReservation method of reservation service with reservationId
            var response = _reservationService.UnbookReservation(reservationId);

            return response.Match(
                response => Ok("Successfully unbooked"),
                error => Problem(statusCode: 400, detail: error.First().Description)
            );
        }

        // GET request to retrieve reservations for the authenticated user
        [HttpGet]
        public IActionResult GetReservationsByUser()
        {
            // Get the email of the authenticated user
            string email = User.FindFirstValue(ClaimTypes.Email);
            // Get reservations for the user from the reservation service
            var reservations = _reservationService.GetReservationsByUser(email);

            // Return reservations mapped to ReservationResponse DTOs using AutoMapper
            return Ok(_mapper.Map<List<ReservationResponse>>(reservations));
        }
    }
}
