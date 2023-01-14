using Alten.Booking.Domain.Users.Entities;
using Alten.Booking.Infra.Data.Sql.Context.Users.Models;
using AutoMapper;

namespace Alten.Booking.Infra.IoC.AutoMapper.Profiles;

public class DomainToModelProfile : Profile
{
    public DomainToModelProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}