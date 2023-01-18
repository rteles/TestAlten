using System.Net;
using Alten.Booking.Application.Bookings.Commands;
using Alten.Booking.Application.Bookings.Queries.Interfaces;
using Alten.Booking.Application.Bookings.ViewModels;
using Alten.Booking.Core.Mediator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alten.Booking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IBookingQueries _bookingQueries;

    public BookingsController(IMediatorHandler mediatorHandler, IBookingQueries bookingQueries)
    {
        _mediatorHandler = mediatorHandler;
        _bookingQueries = bookingQueries;
    }

    [HttpGet("{roomId}", Name = "GetBookingByRoomId")]
    [ProducesResponseType(typeof(BookingViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetBookingByRoomId(int roomId)
    {
        var result = await _bookingQueries.GetByRoomId(roomId);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{checkinDate}/{checkoutDate}", Name = "GetBookingsByRangeDate")]
    [ProducesResponseType(typeof(BookingViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetBookingsByRangeDate([FromRoute] DateTime checkinDate,
        [FromRoute] DateTime checkoutDate)
    {
        var result = await _bookingQueries.Get(checkinDate, checkoutDate);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost(Name = "CreateBooking")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> CreateBooking([FromBody] BookingRoomCommand bookingRoomCommand)
    {
        var result = await _mediatorHandler.SendCommand(bookingRoomCommand);
        return result ? Ok() : BadRequest();
    }

    [HttpPut("{bookingId}", Name = "ModifyBooking")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> ModifyBooking([FromRoute] int bookingId,
        [FromBody] ModifyBookingViewModel modifyBookingViewModel)
    {
        var modifyBookingCommand = new ModifyBookingCommand(bookingId, modifyBookingViewModel.CheckinDate,
            modifyBookingViewModel.CheckoutDate);
        var result = await _mediatorHandler.SendCommand(modifyBookingCommand);
        return result ? Ok() : BadRequest();
    }

    [HttpDelete("{id}", Name = "CancelBooking")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> CancelBooking([FromRoute] int id)
    {
        var result = await _mediatorHandler.SendCommand(new CancelBookingCommand(id));
        return result ? Ok() : BadRequest();
    }
}