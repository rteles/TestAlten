using Alten.Booking.Application.Bookings.ViewModels;

namespace Alten.Booking.Application.Bookings.Queries.Interfaces;

public interface IBookingQueries
{
    Task<IEnumerable<BookingViewModel>> GetByRoomId(int roomId);
    Task<IEnumerable<BookingViewModel>> Get(DateTime checkinDate, DateTime checkoutDate);
}