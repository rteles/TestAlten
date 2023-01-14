using Alten.Booking.Domain.Base;
using Alten.Booking.Domain.Users.Entities;

namespace Alten.Booking.Domain.Users.Repositories;

public interface IUserRepository : IRepositoryDomainBase<User>
{
    Task<IEnumerable<User>> GetAll(bool onlyActives);
}