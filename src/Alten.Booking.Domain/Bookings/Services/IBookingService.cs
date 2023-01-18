namespace Alten.Booking.Domain.Bookings.Services;

public interface IBookingService
{
    Task BookingRoom(int roomId, int userId, DateOnly checkinDate, DateOnly checkoutDate);
    Task ModifyBooking(int id, DateOnly checkinDate, DateOnly checkoutDate);
    Task CancelBooking(int id);
}