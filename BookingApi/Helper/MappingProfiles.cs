using AutoMapper;
using BLL.Services.Auth.Descriptors;
using BLL.Services.Reservations.Descriptors;
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
            CreateMap<Reservation, ReservationResponse>()
                .ForMember(dest => dest.ApartmentName, opt => opt.MapFrom(src => src.Apartment.Name));

            CreateMap<CreateReservationRequest, CreateReservationDescriptor>();
            CreateMap<RegisterRequest, RegisterDescriptor>();
            CreateMap<LoginRequest, LoginDescriptor>();
        }
    }
}
