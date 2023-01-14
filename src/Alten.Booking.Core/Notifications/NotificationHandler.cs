using Alten.Booking.Core.Messages;
using Alten.Booking.Core.Notifications.Interfaces;
using MediatR;

namespace Alten.Booking.Core.Notifications;

public class NotificationHandler : INotificationHandler<Notification>
{
    private readonly INotificationContext _notificationContext;

    public NotificationHandler(INotificationContext notificationContext) =>
        _notificationContext = notificationContext;

    public Task Handle(Notification notification, CancellationToken cancellationToken) =>
        Task.Run(() => _notificationContext.AddNotification(notification), cancellationToken);
}