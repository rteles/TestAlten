using MediatR;

namespace Alten.Booking.Core.Messages;

public class Notification : INotification
{
    public string? Code { get; }

    public string Message { get; }

    public Notification(string message, string? code = null)
    {
        Code = code;
        Message = message;
    }

    public static Notification Create(string message) => new(message);
}