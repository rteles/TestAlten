using Alten.Booking.Core.Messages;

namespace Alten.Booking.Application.Bookings.Commands;

public class ModifyBookingCommand : Command
{
    public ModifyBookingCommand(int bookingId, int roomId, DateTime startDate, DateTime endDate)
    {
        BookingId = bookingId;
        RoomId = roomId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public int BookingId { get; }
    public int RoomId { get; set; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
}