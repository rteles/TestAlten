using Alten.Booking.Application.Hotels.ViewModels;
using Alten.Booking.Application.Users.ViewModels;
using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Domain.Users.Entities;
using AutoMapper;

namespace Alten.Booking.Infra.IoC.AutoMapper.Profiles;

public class DomainToViewModelProfile : Profile
{
    public DomainToViewModelProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<Hotel, HotelViewModel>()
            .ReverseMap()
            .AfterMap((model, hotel) => hotel.Rooms.ForEach(_ => _.Hotel = hotel));
        CreateMap<Room, RoomViewModel>().ReverseMap();
    }
}