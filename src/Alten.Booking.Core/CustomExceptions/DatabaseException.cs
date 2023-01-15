namespace Alten.Booking.Core.CustomExceptions;

public class DatabaseException : DomainException
{
    public DatabaseException(string message) : base(string.Empty, message)
    {
    }
}