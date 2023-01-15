using Alten.Booking.Application.Hotels.ViewModels;
using Alten.Booking.Core.Messages;

namespace Alten.Booking.Application.Hotels.Commands;

public class AddHotelCommand : Command
{
    public AddHotelCommand(HotelViewModel hotel)
    {
        Hotel = hotel;
    }

    public HotelViewModel Hotel { get; }
}