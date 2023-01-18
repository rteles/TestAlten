using Alten.Booking.Core.Messages;

namespace Alten.Booking.Application.Bookings.Commands;

public class ModifyBookingCommand : Command
{
    public ModifyBookingCommand(int bookingId, DateTime checkinDate, DateTime checkoutDate)
    {
        BookingId = bookingId;
        CheckinDate = checkinDate;
        CheckoutDate = checkoutDate;
    }

    public int BookingId { get; }
    public DateTime CheckinDate { get; }
    public DateTime CheckoutDate { get; }
}