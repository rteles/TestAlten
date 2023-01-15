using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Domain.Hotels.Repositories;
using Alten.Booking.Infra.Data.Sql.Base;
using Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alten.Booking.Infra.Data.Sql.Context.Hotels.Repositories;

public class HotelRepository : RepositoryBase<Hotel, HotelModel>,
    IHotelRepository
{
    private readonly HotelBookingContext _context;
    private readonly IMapper _mapper;

    public HotelRepository(HotelBookingContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Hotel>> GetAll(bool onlyActives)
    {
        var hotels = await _context.Hotels.Where(_ => !onlyActives || _.Active)
            .Include(_ => _.Rooms)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<Hotel>>(hotels);
    }
}