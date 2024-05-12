using AutoMapper;
using BookingApi.DTOs;
using BookingApi.Models;

namespace BookingApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterRequest>();
        }
    }
}
