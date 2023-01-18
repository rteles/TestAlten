using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Infra.Data.Sql.Context.Bookings.Models;

namespace Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;

public class RoomModel
{
    public int Id { get; set; }
    public int Number { get; set; }
    public RoomType RoomType { get; set; }
    public double PricePerDay { get; set; }

    public int HotelId { get; set; }
    public HotelModel Hotel { get; set; }
    
    public List<BookingModel> Bookings { get; set; }
}