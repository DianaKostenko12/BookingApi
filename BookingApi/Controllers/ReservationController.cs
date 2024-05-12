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

        [HttpPost]
        public IActionResult BookReservation(CreateReservationRequest request)
        {
            var descriptor = _mapper.Map<CreateReservationDescriptor>(request);
            descriptor.Email = User.FindFirstValue(ClaimTypes.Email);

            var response = _reservationService.BookReservation(descriptor);

            return response.Match(
                response => Ok("Successfully booked"),
                error => Problem(statusCode: 400, detail: error.First().Description)
            );
        }

        [HttpDelete("{reservationId}")]
        public IActionResult UnbookReservation(int reservationId)
        {
            var response = _reservationService.UnbookReservation(reservationId);

            return response.Match(
                response => Ok("Successfully unbooked"),
                error => Problem(statusCode: 400, detail: error.First().Description)
            );
        }

        [HttpGet]
        public IActionResult GetReservationsByUser()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var reservations = _reservationService.GetReservationsByUser(email);

            return Ok(_mapper.Map<List<ReservationResponse>>(reservations));
        }
    }
}
