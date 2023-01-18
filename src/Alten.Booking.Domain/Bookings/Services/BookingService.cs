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

    public async Task BookingRoom(int roomId, int userId, DateTime startDate, DateTime endDate)
    {
        var (checkinDate, checkoutDate) = AdjustPeriodDates(startDate, endDate);
        var stayDuration = ValidateStayDuration(checkoutDate, checkinDate);

        ValidateDaysToCheckin(checkinDate);
        await ValidateRoomBooked(roomId, startDate, endDate);

        var user = await _userRepository.GetById(userId);
        if (user == null)
        {
            throw new DomainException("User not found");
        }

        var room = await GetRoom(roomId);

        var booking = new Entities.Booking
        {
            // User = user,
            // Room = room,
            UserId = userId,
            RoomId = roomId,
            CheckinDate = checkinDate,
            CheckoutDate = checkoutDate,
            PricePerDay = room.PricePerDay,
            TotalPrice = room.PricePerDay * stayDuration.TotalDays,
            Active = true
        };

        await _bookingRepository.Add(booking);
        await _bookingRepository.Commit();
    }

    public async Task ModifyBooking(int id, int roomId, DateTime startDate, DateTime endDate)
    {
        var (checkinDate, checkoutDate) = AdjustPeriodDates(startDate, endDate);
        var stayDuration = ValidateStayDuration(checkoutDate, checkinDate);

        ValidateDaysToCheckin(checkinDate);
        await ValidateRoomBooked(roomId, startDate, endDate);

        var booking = await _bookingRepository.GetById(id);
        if (booking == null)
            throw new DomainException("Booking not found");

        var room = await GetRoom(roomId);

        booking.Room = room;
        booking.CheckinDate = checkinDate;
        booking.CheckoutDate = checkoutDate;
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

    private static (DateTime checkinDate, DateTime checkoutDate) AdjustPeriodDates(DateTime startDate, DateTime endDate)
    {
        var checkinDate = startDate.AddDays(1).Date + new TimeSpan(0, 0, 0);
        var checkoutDate = endDate + new TimeSpan(23, 59, 59);
        return (checkinDate, checkoutDate);
    }

    private static TimeSpan ValidateStayDuration(DateTime checkoutDate, DateTime checkinDate)
    {
        var stayDuration = checkoutDate.Subtract(checkinDate);
        if (stayDuration.TotalDays is < 1 or > 3)
        {
            throw new DomainException("The stay has to be at least 1 day and can't be longer than 3 days");
        }

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

    private async Task ValidateRoomBooked(int roomId, DateTime startDate, DateTime endDate)
    {
        var roomBooked = (await _bookingRepository.Get(roomId, startDate, endDate)).Any();
        if (roomBooked)
        {
            throw new DomainException("Room booked");
        }
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