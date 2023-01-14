using Alten.Booking.Application.Users.Models;

namespace Alten.Booking.Application.Users.Queries.Interfaces;

public interface IUserQueries
{
    Task<UserViewModel?> Get(int id);
    Task<IEnumerable<UserViewModel>> Get(bool onlyActives);
}