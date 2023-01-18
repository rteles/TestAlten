namespace Alten.Booking.Core.CustomExceptions;

public class DomainException : Exception
{
    public string ErrorCode { get; }

    public DomainException(string message) : this(string.Empty, message)
    {
    }

    protected DomainException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}