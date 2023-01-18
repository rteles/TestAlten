namespace Alten.Booking.Domain.Bookings.Services;

public interface IBookingService
{
    Task BookingRoom(int roomId, int userId, DateTime startDate, DateTime endDate);
    Task ModifyBooking(int id, int roomId, DateTime startDate, DateTime endDate);
    Task CancelBooking(int id);
}