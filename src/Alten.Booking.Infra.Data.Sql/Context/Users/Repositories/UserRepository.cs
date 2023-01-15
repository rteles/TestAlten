using Alten.Booking.Domain.Users.Entities;
using Alten.Booking.Domain.Users.Repositories;
using Alten.Booking.Infra.Data.Sql.Base;
using Alten.Booking.Infra.Data.Sql.Context.Users.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alten.Booking.Infra.Data.Sql.Context.Users.Repositories;

public class UserRepository : RepositoryBase<User, UserModel>,
    IUserRepository
{
    private readonly HotelBookingContext _context;
    private readonly IMapper _mapper;

    public UserRepository(HotelBookingContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetAll(bool onlyActives) =>
        _mapper.Map<IEnumerable<User>>(await _context.Users.Where(_ => !onlyActives || _.Active).AsNoTracking()
            .ToListAsync());
}