using Alten.Booking.Domain.Base;
using Alten.Booking.Domain.Hotels.Entities;

namespace Alten.Booking.Domain.Hotels.Repositories;

public interface IHotelRepository : IRepositoryDomainBase<Hotel>
{
    Task<IEnumerable<Hotel>> GetAll(bool onlyActives);
}