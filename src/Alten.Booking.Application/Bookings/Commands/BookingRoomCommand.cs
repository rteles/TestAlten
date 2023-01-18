using Alten.Booking.Core.Messages;

namespace Alten.Booking.Application.Bookings.Commands;

public class BookingRoomCommand : Command
{
    public BookingRoomCommand(int roomId, int userId, DateTime startDate, DateTime endDate)
    {
        RoomId = roomId;
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public int RoomId { get; }
    public int UserId { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
}