using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Core.Messages;
using MediatR;

namespace Alten.Booking.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator) => _mediator = mediator;
    
    public async Task SendNotification<T>(params T[] notifications)
        where T : Notification => await SendNotification(default, notifications); 
    
    public async Task SendNotification<T>(CancellationToken cancellationToken, params T[] notifications)
        where T : Notification =>
        await Task.WhenAll(notifications.Select(notification => _mediator.Publish(notification, cancellationToken)));

    public async Task<bool> SendCommand<T>(T command) where T : Command => await _mediator.Send(command);
}