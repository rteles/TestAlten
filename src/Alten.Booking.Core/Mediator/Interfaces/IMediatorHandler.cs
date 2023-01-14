using Alten.Booking.Core.Messages;

namespace Alten.Booking.Core.Mediator.Interfaces;

public interface IMediatorHandler
{
    Task SendNotification<T>(CancellationToken cancellationToken = default, params T[] notifications)
        where T : Notification;

    Task<TResponse> SendCommand<T, TResponse>(T command, CancellationToken cancellationToken = default)
        where T : Command<TResponse>;

    Task SendCommand<T>(T command, CancellationToken cancellationToken = default) where T : Command;
}