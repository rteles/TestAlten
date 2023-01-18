namespace Alten.Booking.Application.Bookings.ViewModels;

public record BookingViewModel(int Id, int UserId, int RoomId, DateTime CheckinDate, DateTime CheckoutDate,
    double PricePerDay, double TotalPrice, bool Active);