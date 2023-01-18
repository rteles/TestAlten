using Alten.Booking.Core.CustomExceptions;
using Alten.Booking.Domain.Bookings.Repositories;
using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Domain.Hotels.Repositories;
using Alten.Booking.Domain.Users.Repositories;

namespace Alten.Booking.Domain.Bookings.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;

    public BookingService(IBookingRepository bookingRepository,
        IUserRepository userRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _roomRepository = roomRepository;
    }

    public async Task BookingRoom(int roomId, int userId, DateOnly checkinDate, DateOnly checkoutDate)
    {
        var (checkinDateTime, checkoutDateTime) = AdjustPeriodDates(checkinDate, checkoutDate);
        var stayDuration = ValidateStayDuration(checkinDateTime, checkoutDateTime);

        ValidateDaysToCheckin(checkinDateTime);
        await ValidateRoomBooked(checkinDateTime, checkoutDateTime, roomId);

        var user = await _userRepository.GetById(userId);
        if (user == null)
        {
            throw new DomainException("User not found");
        }

        var room = await GetRoom(roomId);

        var booking = new Entities.Booking
        {
            UserId = userId,
            RoomId = roomId,
            CheckinDate = checkinDateTime,
            CheckoutDate = checkoutDateTime,
            PricePerDay = room.PricePerDay,
            TotalPrice = room.PricePerDay * stayDuration.TotalDays,
            Active = true
        };

        await _bookingRepository.Add(booking);
        await _bookingRepository.Commit();
    }

    public async Task ModifyBooking(int id, DateOnly checkinDate, DateOnly checkoutDate)
    {
        var (checkinDateTime, checkoutDateTime) = AdjustPeriodDates(checkinDate, checkoutDate);
        var stayDuration = ValidateStayDuration(checkinDateTime, checkoutDateTime);

        ValidateDaysToCheckin(checkinDateTime);
        
        var booking = await _bookingRepository.GetById(id);
        if (booking is not { Active: true })
            throw new DomainException("Booking not found");
        
        await ValidateRoomBooked(checkinDateTime, checkoutDateTime, booking.RoomId, id);
        var room = await GetRoom(booking.RoomId);
        
        booking.CheckinDate = checkinDateTime;
        booking.CheckoutDate = checkoutDateTime;
        booking.TotalPrice = room.PricePerDay * stayDuration.TotalDays;

        _bookingRepository.Update(booking);
        await _bookingRepository.Commit();
    }

    public async Task CancelBooking(int id)
    {
        var booking = await _bookingRepository.GetById(id);
        if (booking == null)
            throw new DomainException("Booking not found");

        booking.Active = false;

        _bookingRepository.Update(booking);
        await _bookingRepository.Commit();
    }

    #region Private Methods

    private static (DateTime checkinDateTime, DateTime checkoutDateTime) AdjustPeriodDates(DateOnly checkinDate,
        DateOnly checkoutDate)
    {
        var checkinDateTime = checkinDate.ToDateTime(new TimeOnly(0, 0, 0));
        var checkoutDateTime = checkoutDate.ToDateTime(new TimeOnly(23, 59, 59));
        return (checkinDateTime, checkoutDateTime);
    }

    private static TimeSpan ValidateStayDuration(DateTime checkinDate, DateTime checkoutDate)
    {
        if (checkinDate <= DateTime.Now)
            throw new DomainException("The check-in date can't be less or equal than current day");
        
        var stayDuration = checkoutDate.Subtract(checkinDate);
        if (stayDuration.Days is < 1 or > 3)
            throw new DomainException("The stay has to be at least 1 day and can't be longer than 3 days");

        return stayDuration;
    }

    private static void ValidateDaysToCheckin(DateTime checkinDate)
    {
        var daysToCheckin = checkinDate.Subtract(DateTime.Now);
        if (daysToCheckin.TotalDays > 30)
        {
            throw new DomainException("The room can’t be reserved more than 30 days in advance");
        }
    }

    private async Task ValidateRoomBooked(DateTime startDate, DateTime endDate, int roomId, int? bookingId = null)
    {
        var bookings = await _bookingRepository.Get(roomId, startDate, endDate);
        var roomBooked = bookings.Any(_ => (!bookingId.HasValue || _.Id != bookingId) && _.Active);

        if (roomBooked)
            throw new DomainException("Room booked");
    }

    private async Task<Room?> GetRoom(int roomId)
    {
        var room = await _roomRepository.GetById(roomId);
        if (room == null)
        {
            throw new DomainException("Room not found");
        }

        return room;
    }

    #endregion
}