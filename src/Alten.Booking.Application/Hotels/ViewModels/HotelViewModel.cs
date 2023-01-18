namespace Alten.Booking.Application.Hotels.ViewModels;

public record HotelViewModel(int Id, string Name, string Description, DateTime CreateDate, string Country, string State,
    string City, string Address, List<RoomViewModel> Rooms);