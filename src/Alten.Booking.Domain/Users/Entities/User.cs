using Alten.Booking.Domain.Users.Entities.Base;

namespace Alten.Booking.Domain.Users.Entities;

public class User : Person
{
    public User() : base(default, string.Empty, string.Empty, DateTime.MinValue)
    {
    }

    public User(int id, string name, string lastName, DateTime birthDate, string email, bool active)
        : base(id, name, lastName, birthDate)
    {
        Email = email;
        Active = active;
    }

    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
}