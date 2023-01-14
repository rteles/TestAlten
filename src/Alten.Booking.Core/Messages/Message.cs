namespace Alten.Booking.Core.Messages;

public abstract class Message
{
    public string MessageType { get; }

    public DateTime Timestamp { get; }

    protected Message()
    {
        MessageType = GetType().Name;
        Timestamp = DateTime.UtcNow;
    }
}