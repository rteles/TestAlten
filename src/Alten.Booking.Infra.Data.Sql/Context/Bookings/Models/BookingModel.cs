using Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;
using Alten.Booking.Infra.Data.Sql.Context.Users.Models;

namespace Alten.Booking.Infra.Data.Sql.Context.Bookings.Models;

public class BookingModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckinDate { get; set; }
    public DateTime CheckoutDate { get; set; }
    public double PricePerDay { get; set; }
    public double TotalPrice { get; set; }
    public bool Active { get; set; }
    
    public UserModel User { get; init; }
    public RoomModel Room { get; set; }
}