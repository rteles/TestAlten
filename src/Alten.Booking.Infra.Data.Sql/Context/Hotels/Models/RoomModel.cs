namespace Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;

public class RoomModel
{
    public int Id { get; set; }
    public int Number { get; set; }
    public double PricePerDay { get; set; }

    public int HotelId { get; set; }
    public HotelModel Hotel { get; set; }
}