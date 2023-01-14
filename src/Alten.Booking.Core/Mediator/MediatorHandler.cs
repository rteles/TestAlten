using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Core.Messages;
using MediatR;

namespace Alten.Booking.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator) => _mediator = mediator;

    public async Task SendNotification<T>(CancellationToken cancellationToken = default, params T[] notifications)
        where T : Notification =>
        await Task.WhenAll(notifications.Select(notification => _mediator.Publish(notification, cancellationToken)));

    public async Task<TResponse> SendCommand<T, TResponse>(T command, CancellationToken cancellationToken = default)
        where T : Command<TResponse> => await _mediator.Send(command, cancellationToken);
    
    public async Task SendCommand<T>(T command, CancellationToken cancellationToken = default) where T : Command =>
        await _mediator.Send(command, cancellationToken);
}