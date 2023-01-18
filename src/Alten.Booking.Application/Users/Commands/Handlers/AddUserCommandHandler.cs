using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Core.Messages;
using Alten.Booking.Domain.Base;
using Alten.Booking.Domain.Users.Entities;
using Alten.Booking.Domain.Users.Repositories;
using Alten.Booking.Infra.Adapters.Logger;
using AutoMapper;
using MediatR;

namespace Alten.Booking.Application.Users.Commands.Handlers;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
{
    private readonly ILogAdapter _logger;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IModelValidator<User> _userValidator;
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(ILogAdapter logger, IMapper mapper, IMediatorHandler mediatorHandler,
        IModelValidator<User> userValidator,
        IUserRepository userRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
        _userValidator = userValidator;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<User>(command.User);

        if (_userValidator.IsValid(newUser, out var errors))
        {
            newUser.Active = true;
            await _userRepository.Add(newUser);
            await _userRepository.Commit();
            return true;
        }

        await _mediatorHandler.SendNotification(cancellationToken, errors.Select(Notification.Create).ToArray());
        return false;
    }
}