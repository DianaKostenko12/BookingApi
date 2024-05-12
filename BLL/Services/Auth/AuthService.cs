using BLL.Services.Auth.Descriptors;
using BookingApi.Models;
using DAL.Repositories.Users;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public ErrorOr<Success> Register(RegisterDescriptor descriptor)
        {
            var users = _userRepository.GetUserByEmail(descriptor.Email);
            if (users != null)
            {
                return Error.NotFound(description: "User with that email already exists");
            }

            CreatePasswordHash(descriptor.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var registerUser = new User
            {
                FirstName = descriptor.FirstName,
                LastName = descriptor.LastName,
                Email = descriptor.Email,
                PasswordHash = JsonConvert.SerializeObject(passwordHash),
                PasswordSalt = JsonConvert.SerializeObject(passwordSalt)
            };

            _userRepository.CreateUser(registerUser);
            _userRepository.Save();

            return Result.Success;
        }

        public ErrorOr<string> Login(LoginDescriptor descriptor)
        {
            if (_userRepository.GetUserByEmail(descriptor.Email) == null)
            {
                return Error.NotFound(description: "User is not found");
            }

            User user = _userRepository.GetUserByEmail(descriptor.Email);
            byte[] passwordHash = JsonConvert.DeserializeObject<byte[]>(user.PasswordHash);
            byte[] passwordSalt = JsonConvert.DeserializeObject<byte[]>(user.PasswordSalt);

            if (!VerifyPaswordHash(descriptor.Password, passwordHash, passwordSalt))
            {
                return Error.Failure(description: "Wrong password");
            }

            return CreateToken(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPaswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
