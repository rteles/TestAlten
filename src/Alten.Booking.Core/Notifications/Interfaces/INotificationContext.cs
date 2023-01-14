using Alten.Booking.Core.Messages;

namespace Alten.Booking.Core.Notifications.Interfaces;

public interface INotificationContext
{
    bool HasNotifications { get; }
    IEnumerable<Notification> Notifications { get; }
    void AddNotification(params Notification[] notification);
}