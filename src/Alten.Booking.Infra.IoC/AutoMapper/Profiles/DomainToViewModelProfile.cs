using Alten.Booking.Application.Users.Models;
using Alten.Booking.Domain.Users.Entities;
using AutoMapper;

namespace Alten.Booking.Infra.IoC.AutoMapper.Profiles;

public class DomainToViewModelProfile : Profile
{
    public DomainToViewModelProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
    }
}