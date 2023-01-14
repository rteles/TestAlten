using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Domain.Users.Entities;
using Alten.Booking.Domain.Users.Repositories;
using Alten.Booking.Infra.Data.Sql.Base;
using Alten.Booking.Infra.Data.Sql.Context.Users.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alten.Booking.Infra.Data.Sql.Context.Users.Repositories.Users;

public class UserRepository : RepositoryBase<User, UserModel>,
    IUserRepository
{
    private readonly HotelBookingContext _context;
    private readonly IMapper _mapper;

    public UserRepository(HotelBookingContext usersContext, IMapper mapper, IMediatorHandler mediatorHandler)
        : base(usersContext, mapper, mediatorHandler)
    {
        _context = usersContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetAll(bool onlyActives)
    {
        var users = await _context.Users.Where(_ => !onlyActives || _.Active).AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<User>>(users);
    }
}