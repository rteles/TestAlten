using Alten.Booking.Domain.Base;

namespace Alten.Booking.Domain.Bookings.Repositories;

public interface IBookingRepository : IRepositoryDomainBase<Entities.Booking>
{
    Task<IEnumerable<Entities.Booking>> Get(int roomId, DateTime checkinDate, DateTime checkoutDate);
    Task<IEnumerable<Entities.Booking>> GetByRoomId(int roomId);
    Task<IEnumerable<Entities.Booking>> Get(DateTime checkinDate, DateTime checkoutDate);
}