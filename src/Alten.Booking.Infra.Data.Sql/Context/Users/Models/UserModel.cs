using Alten.Booking.Infra.Data.Sql.Context.Bookings.Models;

namespace Alten.Booking.Infra.Data.Sql.Context.Users.Models;

public class UserModel
{
    public UserModel()
    {
    }

    public UserModel(int id, string name, string lastName, string cpf, DateTime birthDate, string email)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Cpf = cpf;
        BirthDate = birthDate;
        Email = email;
    }

    public int Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public DateTime BirthDate { get; init; } = DateTime.MinValue;
    public string Email { get; init; } = string.Empty;
    public bool Active { get; set; }
    
    public List<BookingModel> Bookings { get; set; }
}