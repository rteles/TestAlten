using Alten.Booking.Core.CustomExceptions;
using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Core.Messages;
using Alten.Booking.Domain.Base;
using Alten.Booking.Domain.Bookings.Services;
using Alten.Booking.Infra.Adapters.Logger;
using MediatR;

namespace Alten.Booking.Application.Bookings.Commands.Handlers;

public class BookingCommandHandler :
    IRequestHandler<BookingRoomCommand, bool>,
    IRequestHandler<ModifyBookingCommand, bool>,
    IRequestHandler<CancelBookingCommand, bool>
{
    private readonly ILogAdapter _logger;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IModelValidator<BookingRoomCommand> _bookingRoomCommandValidator;
    private readonly IModelValidator<ModifyBookingCommand> _modifyBookingCommandValidator;
    private readonly IModelValidator<CancelBookingCommand> _cancelBookingCommandValidator;
    private readonly IBookingService _bookingService;

    public BookingCommandHandler(ILogAdapter logger, IMediatorHandler mediatorHandler,
        IModelValidator<BookingRoomCommand> bookingRoomCommandValidator,
        IModelValidator<ModifyBookingCommand> modifyBookingCommandValidator,
        IModelValidator<CancelBookingCommand> cancelBookingCommandValidator,
        IBookingService bookingService)
    {
        _logger = logger;
        _mediatorHandler = mediatorHandler;
        _bookingService = bookingService;
        _bookingRoomCommandValidator = bookingRoomCommandValidator;
        _modifyBookingCommandValidator = modifyBookingCommandValidator;
        _cancelBookingCommandValidator = cancelBookingCommandValidator;
    }

    public async Task<bool> Handle(BookingRoomCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (_bookingRoomCommandValidator.IsValid(command, out var errors))
            {
                await _bookingService.BookingRoom(command.RoomId, command.UserId,
                    DateOnly.FromDateTime(command.CheckinDate), DateOnly.FromDateTime(command.CheckoutDate));
                return true;
            }

            await _mediatorHandler.SendNotification(cancellationToken, errors.Select(Notification.Create).ToArray());
        }
        catch (DomainException domainException)
        {
            await _mediatorHandler.SendNotification(cancellationToken, Notification.Create(domainException.Message));
            _logger.Error(nameof(BookingRoomCommand), domainException);
        }

        return false;
    }

    public async Task<bool> Handle(ModifyBookingCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (_modifyBookingCommandValidator.IsValid(command, out var errors))
            {
                await _bookingService.ModifyBooking(command.BookingId,
                    DateOnly.FromDateTime(command.CheckinDate),
                    DateOnly.FromDateTime(command.CheckoutDate));
                return true;
            }

            await _mediatorHandler.SendNotification(cancellationToken, errors.Select(Notification.Create).ToArray());
        }
        catch (DomainException domainException)
        {
            await _mediatorHandler.SendNotification(cancellationToken, Notification.Create(domainException.Message));
            _logger.Error(nameof(ModifyBookingCommand), domainException);
        }

        return false;
    }

    public async Task<bool> Handle(CancelBookingCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (_cancelBookingCommandValidator.IsValid(command, out var errors))
            {
                await _bookingService.CancelBooking(command.BookingId);
                return true;
            }

            await _mediatorHandler.SendNotification(cancellationToken, errors.Select(Notification.Create).ToArray());
        }
        catch (DomainException domainException)
        {
            await _mediatorHandler.SendNotification(cancellationToken, Notification.Create(domainException.Message));
            _logger.Error(nameof(CancelBookingCommand), domainException);
        }

        return false;
    }
}