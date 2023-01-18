using Alten.Booking.Application.Bookings.Queries.Interfaces;
using Alten.Booking.Application.Bookings.ViewModels;
using Alten.Booking.Domain.Bookings.Repositories;
using Alten.Booking.Infra.Adapters.Logger;
using AutoMapper;

namespace Alten.Booking.Application.Bookings.Queries;

public class BookingQueries : IBookingQueries
{
    private readonly IMapper _mapper;
    private readonly ILogAdapter _logAdapter;
    private readonly IBookingRepository _bookingRepository;

    public BookingQueries(IMapper mapper, ILogAdapter logAdapter, IBookingRepository bookingRepository)
    {
        _mapper = mapper;
        _logAdapter = logAdapter;
        _bookingRepository = bookingRepository;
    }

    public async Task<IEnumerable<BookingViewModel>> GetByRoomId(int roomId) =>
        _mapper.Map<IEnumerable<BookingViewModel>>(await _bookingRepository.GetByRoomId(roomId));

    public async Task<IEnumerable<BookingViewModel>> Get(DateTime checkinDate, DateTime checkoutDate)
    {
        return _mapper.Map<IEnumerable<BookingViewModel>>(await _bookingRepository.Get(checkinDate, checkoutDate));
    }
}