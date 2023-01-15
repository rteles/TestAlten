using Microsoft.AspNetCore.Mvc;

namespace Alten.Booking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    // GET: api/Example/5
    [HttpGet("{id}", Name = "GetBooking")]
    public string GetBooking(int id)
    {
        return "value";
    }
    
    [HttpGet("{roomId}", Name = "GetBookingByRoomId")]
    public string GetBookingByRoomId(int roomId)
    {
        return "value";
    }

    [HttpGet("{startDate}/{endDate}", Name = "GetBookingByRangeDate")]
    public string GetBookingByRangeDate(DateTime startDate, DateTime endDate)
    {
        return "value";
    }
    
    // // GET: api/Example
    // [HttpGet(Name = "GetBookings")]
    // public IEnumerable<string> GetBookings()
    // {
    //     return new string[] { "value1", "value2" };
    // }
    
    // POST: api/Example
    [HttpPost(Name = "CreateBooking")]
    public void CreateBooking([FromBody] string value)
    {
    }

    // PUT: api/Example/5
    [HttpPut("{id}", Name = "ModifyBooking")]
    public void ModifyBooking(int id, [FromBody] string value)
    {
    }

    // DELETE: api/Example/5
    [HttpDelete("{id}", Name = "CancelBooking")]
    public void CancelBooking(int id)
    {
    }
}