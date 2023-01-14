using Alten.Booking.Application.Users.Models;
using Alten.Booking.Core.Messages;

namespace Alten.Booking.Application.Users.Commands;

public class AddUserCommand : Command
{
    public AddUserCommand(UserViewModel user) => User = user;

    public UserViewModel User { get; }
}