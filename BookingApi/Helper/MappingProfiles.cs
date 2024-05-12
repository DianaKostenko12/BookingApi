using AutoMapper;
using BookingApi.DTOs;
using BookingApi.Models;

namespace BookingApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, ReservationResponse>();
            CreateMap<User, LoginRequest>();
            CreateMap<Reservation, CreateReservationRequest>();
            CreateMap<Reservation, ReservationResponse>();
            CreateMap<Apartment, ApartmentResponse>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Reservation, ReservationResponse>()
                    .ForMember(dest => dest.ApartmentName, opt => opt.MapFrom(src => src.Apartment.Name));
            });
        }
    }
}
