namespace Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;

public class HotelModel
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; set; }
    public DateTime CreateDate { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
    public string City { get; init; } 
    public string Address { get; init; }
    public bool Active { get; set; }

    public List<RoomModel> Rooms { get; set; }
}