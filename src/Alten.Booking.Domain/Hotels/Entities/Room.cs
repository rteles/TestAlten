namespace Alten.Booking.Domain.Hotels.Entities;

public class Room
{
    public Hotel Hotel { get; set; }
    public int Number { get; set; }
    public double PricePerDay { get; set; }
}