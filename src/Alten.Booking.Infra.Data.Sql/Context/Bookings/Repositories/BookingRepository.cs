using Alten.Booking.Domain.Bookings.Repositories;
using Alten.Booking.Infra.Data.Sql.Base;
using Alten.Booking.Infra.Data.Sql.Context.Bookings.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alten.Booking.Infra.Data.Sql.Context.Bookings.Repositories;

public class BookingRepository : RepositoryBase<Domain.Bookings.Entities.Booking, BookingModel>,
    IBookingRepository
{
    private readonly HotelBookingContext _context;
    private readonly IMapper _mapper;

    public BookingRepository(HotelBookingContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Domain.Bookings.Entities.Booking>> Get(int roomId, DateTime checkinDate,
        DateTime checkoutDate)
    {
        var bookings = await _context.Bookings
            .Where(_ => _.RoomId == roomId && _.CheckinDate >= checkinDate && _.CheckoutDate <= checkoutDate)
            .Include(_ => _.Room)
            .Include(_ => _.User)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<Domain.Bookings.Entities.Booking>>(bookings);
    }

    public async Task<IEnumerable<Domain.Bookings.Entities.Booking>> GetByRoomId(int roomId)
    {
        var bookings = await _context.Bookings.Where(_ => _.RoomId == roomId)
            .Include(_ => _.Room)
            .Include(_ => _.User)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<Domain.Bookings.Entities.Booking>>(bookings);
    }

    public async Task<IEnumerable<Domain.Bookings.Entities.Booking>> Get(DateTime checkinDate, DateTime checkoutDate)
    {
        var bookings = await _context.Bookings.Where(_ => _.CheckinDate >= checkinDate
                                                          && _.CheckoutDate <= checkoutDate)
            .Include(_ => _.Room)
            .Include(_ => _.User)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<Domain.Bookings.Entities.Booking>>(bookings);
    }
}