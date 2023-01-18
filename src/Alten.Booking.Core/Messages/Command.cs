using MediatR;

namespace Alten.Booking.Core.Messages;

public abstract class Command : IRequest<bool>
{
}