using MediatR;

namespace Alten.Booking.Core.Messages;

public abstract class Command<T> : Message, IRequest<T>
{
}

public abstract class Command : Message, IRequest
{
}