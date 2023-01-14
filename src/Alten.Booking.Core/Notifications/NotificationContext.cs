using Alten.Booking.Core.Messages;
using Alten.Booking.Core.Notifications.Interfaces;

namespace Alten.Booking.Core.Notifications;

public class NotificationContext : INotificationContext
{
    private readonly List<Notification> _notifications;

    public bool HasNotifications => _notifications.Any();

    public IEnumerable<Notification> Notifications => _notifications;

    public NotificationContext() => _notifications = new List<Notification>();

    public void AddNotification(params Notification[] notification) =>
        _notifications.AddRange(notification);
}