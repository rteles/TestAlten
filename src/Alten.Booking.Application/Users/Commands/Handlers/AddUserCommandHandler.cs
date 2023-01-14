using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Core.Messages;
using Alten.Booking.Domain.Base;
using Alten.Booking.Domain.Users.Entities;
using Alten.Booking.Domain.Users.Repositories;
using AutoMapper;
using MediatR;

namespace Alten.Booking.Application.Users.Commands.Handlers;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IModelValidator<User> _userValidator;
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IMapper mapper, IMediatorHandler mediatorHandler, IModelValidator<User> userValidator,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
        _userValidator = userValidator;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(command.User);

        if (_userValidator.IsValid(user, out var errors))
        {
            await _userRepository.Add(user);
            await _userRepository.Commit();
        }
        
        await _mediatorHandler.SendNotification(cancellationToken, errors.Select(Notification.Create).ToArray());
        return await Unit.Task;
    }
}