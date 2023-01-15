using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Domain.Users.Entities;
using Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;
using Alten.Booking.Infra.Data.Sql.Context.Users.Models;
using AutoMapper;

namespace Alten.Booking.Infra.IoC.AutoMapper.Profiles;

public class DomainToModelProfile : Profile
{
    public DomainToModelProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<Hotel, HotelModel>().ReverseMap();
        CreateMap<Room, RoomModel>().ReverseMap();
    }
}