namespace Alten.Booking.Core.CustomExceptions;

public abstract class DomainException : Exception
{
    public string ErrorCode { get; init; }

    protected DomainException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}