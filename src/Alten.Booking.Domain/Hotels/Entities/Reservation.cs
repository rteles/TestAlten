using Alten.Booking.Domain.Users.Entities;

namespace Alten.Booking.Domain.Hotels.Entities;

public class Reservation
{
    public Hotel Hotel { get; set; }
    public Room Room { get; set; }
    public User User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
}