using Alten.Booking.Core.Messages;

namespace Alten.Booking.Core.Mediator.Interfaces;

public interface IMediatorHandler
{
    Task SendNotification<T>(params T[] notifications)
        where T : Notification;

    Task SendNotification<T>(CancellationToken cancellationToken = default, params T[] notifications)
        where T : Notification;

    Task<bool> SendCommand<T>(T command) where T : Command;
}