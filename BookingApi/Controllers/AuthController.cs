using AutoMapper;
using BLL.Services.Auth;
using BLL.Services.Auth.Descriptors;
using BookingApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers
{
    [ApiController, Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(
            IAuthService authService,
            IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var descriptor = _mapper.Map<RegisterDescriptor>(request);
            var response = _authService.Register(descriptor);

            return response.Match(
                response => Ok("Successfully registered"),
                error => Problem(statusCode: 400, detail: error.First().Description)
            );
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginRequest request)
        {
            var descriptor = _mapper.Map<LoginDescriptor>(request);
            var response = _authService.Login(descriptor);

            return response.Match(
                token => Ok(token),
                error => Problem(statusCode: 400, detail: error.First().Description)
            );
        }
    }
}
