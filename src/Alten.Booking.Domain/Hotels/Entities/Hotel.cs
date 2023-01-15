namespace Alten.Booking.Domain.Hotels.Entities;

public class Hotel
{
    public string Name { get; init; }
    public string Description { get; set; }
    public DateTime CreateDate { get; init; }
    public string Country { get; init; }
    public string State { get; init; }
    public string City { get; init; }
    public string Address { get; init; }
    public bool Active { get; set; }

    public List<Room> Rooms { get; set; }
}