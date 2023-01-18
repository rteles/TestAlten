using System.Net;
using Alten.Booking.Application.Hotels.Commands;
using Alten.Booking.Application.Hotels.Queries.Interfaces;
using Alten.Booking.Application.Hotels.ViewModels;
using Alten.Booking.Core.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alten.Booking.API.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IHotelQueries _hotelQueries;

    public HotelsController(IMediatorHandler mediatorHandler, IHotelQueries hotelQueries)
    {
        _mediatorHandler = mediatorHandler;
        _hotelQueries = hotelQueries;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(HotelViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetHotelById(int id)
    {
        var result = await _hotelQueries.Get(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HotelViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllHotels()
    {
        var result = await _hotelQueries.GetAll(false);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> CreateHotel([FromBody] HotelViewModel newHotel)
    {
        var result = await _mediatorHandler.SendCommand(new AddHotelCommand(newHotel));
        return result ? Ok() : BadRequest();
    }
}