using Alten.Booking.Core.Messages;

namespace Alten.Booking.Application.Bookings.Commands;

public class CancelBookingCommand : Command
{
    public CancelBookingCommand(int bookingId)
    {
        BookingId = bookingId;
    }

    public int BookingId { get; }
}