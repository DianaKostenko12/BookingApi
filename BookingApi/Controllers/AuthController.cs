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

        // POST request to register a new user
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            // Map RegisterRequest DTO to RegisterDescriptor using AutoMapper
            var descriptor = _mapper.Map<RegisterDescriptor>(request);
            var response = _authService.Register(descriptor);

            // Return response based on the result of registration
            return response.Match(
                response => Ok("Successfully registered"),
                error => Problem(statusCode: 400, detail: error.First().Description)
            );
        }

        // POST request to authenticate and log in a user
        [HttpPost("login")]
        public ActionResult<string> Login(LoginRequest request)
        {
            // Map LoginRequest DTO to LoginDescriptor using AutoMapper
            var descriptor = _mapper.Map<LoginDescriptor>(request);
            var response = _authService.Login(descriptor);

            // Return response based on the result of login
            return response.Match(
                token => Ok(token),
                error => Problem(statusCode: 400, detail: error.First().Description)
            );
        }
    }
}
