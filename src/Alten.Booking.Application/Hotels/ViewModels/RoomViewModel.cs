using Alten.Booking.Domain.Hotels.Entities;

namespace Alten.Booking.Application.Hotels.ViewModels;

public record RoomViewModel(int Id, int Number, double PricePerDay, RoomType RoomType);