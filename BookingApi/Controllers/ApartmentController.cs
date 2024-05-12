using AutoMapper;
using BookingApi.DTOs;
using DAL.Repositories.Accommodations;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers
{
    [ApiController, Route("apartments")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;
        public ApartmentController(IApartmentRepository apartmentRepository, IMapper mapper)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllApartments() 
        {
            var apartments = _mapper.Map<List<ApartmentResponse>>(_apartmentRepository.GetAll());
            return Ok(apartments);
        }
    }
}
