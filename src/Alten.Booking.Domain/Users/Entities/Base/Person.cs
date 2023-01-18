namespace Alten.Booking.Domain.Users.Entities.Base;

public abstract class Person
{
    protected Person(int id, string name, string lastName, DateTime birthDate)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public int Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string LastName { get; init; }
    public DateTime BirthDate { get; init; }
}