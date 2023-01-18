using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Core.Messages;
using Alten.Booking.Domain.Base;
using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Domain.Hotels.Repositories;
using AutoMapper;
using MediatR;

namespace Alten.Booking.Application.Hotels.Commands.Handlers;

public class AddHotelCommandHandler : IRequestHandler<AddHotelCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IModelValidator<Hotel> _hotelValidator;
    private readonly IHotelRepository _hotelRepository;

    public AddHotelCommandHandler(IMapper mapper, IMediatorHandler mediatorHandler,
        IModelValidator<Hotel> hotelValidator, IHotelRepository hotelRepository)
    {
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
        _hotelValidator = hotelValidator;
        _hotelRepository = hotelRepository;
    }

    public async Task<bool> Handle(AddHotelCommand command, CancellationToken cancellationToken)
    {
        var newHotel = _mapper.Map<Hotel>(command.Hotel);

        if (_hotelValidator.IsValid(newHotel, out var errors))
        {
            newHotel.Active = true;
            await _hotelRepository.Add(newHotel);
            await _hotelRepository.Commit();
            return true;
        }

        await _mediatorHandler.SendNotification(cancellationToken, errors.Select(Notification.Create).ToArray());
        return false;
    }
}