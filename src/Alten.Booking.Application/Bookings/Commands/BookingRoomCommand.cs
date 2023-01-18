using Alten.Booking.Core.Messages;

namespace Alten.Booking.Application.Bookings.Commands;

public class BookingRoomCommand : Command
{
    public BookingRoomCommand(int roomId, int userId, DateTime checkinDate, DateTime checkoutDate)
    {
        RoomId = roomId;
        UserId = userId;
        CheckinDate = checkinDate;
        CheckoutDate = checkoutDate;
    }

    public int RoomId { get; }
    public int UserId { get; }
    public DateTime CheckinDate { get; }
    public DateTime CheckoutDate { get; }
}