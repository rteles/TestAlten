using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Domain.Users.Entities;

namespace Alten.Booking.Domain.Bookings.Entities;

public class Booking
{
    public int Id { get; set; }
    public User User { get; init; }
    public int UserId { get; set; }
    public Room Room { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }
    public double PricePerDay { get; set; }
    public double TotalPrice { get; set; }
    public bool Active { get; set; }
}