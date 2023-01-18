using System.Net;
using Alten.Booking.Application.Users.Commands;
using Alten.Booking.Application.Users.Queries.Interfaces;
using Alten.Booking.Application.Users.ViewModels;
using Alten.Booking.Core.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alten.Booking.API.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IUserQueries _userQueries;

    public UsersController(IMediatorHandler mediatorHandler, IUserQueries userQueries)
    {
        _mediatorHandler = mediatorHandler;
        _userQueries = userQueries;
    }

    [HttpGet("{id:int}", Name = nameof(GetUserById))]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _userQueries.Get(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetActiveUsers()
    {
        var result = await _userQueries.Get(true);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> CreateUser([FromBody] UserViewModel newUser)
    {
        var result = await _mediatorHandler.SendCommand(new AddUserCommand(newUser));
        return result ? Ok() : BadRequest();
    }
}