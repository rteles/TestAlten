using System.Net;
using Alten.Booking.Application.Users.Commands;
using Alten.Booking.Application.Users.Models;
using Alten.Booking.Application.Users.Queries.Interfaces;
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

    [HttpGet("{id:int}", Name = "GetById")]
    [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _userQueries.Get(id);
        if (result != null)
            return Ok(result);

        return NotFound();
    }

    [HttpGet(Name = "GetActives")]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetActives()
    {
        var result = await _userQueries.Get(true);
        if (result.Any())
            return Ok(result);

        return NotFound();
    }

    [HttpPost]
    public async Task Post([FromBody] UserViewModel user) =>
        await _mediatorHandler.SendCommand(new AddUserCommand(user));
}