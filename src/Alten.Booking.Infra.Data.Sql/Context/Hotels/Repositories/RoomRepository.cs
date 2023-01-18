using Alten.Booking.Domain.Hotels.Entities;
using Alten.Booking.Domain.Hotels.Repositories;
using Alten.Booking.Infra.Data.Sql.Base;
using Alten.Booking.Infra.Data.Sql.Context.Hotels.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alten.Booking.Infra.Data.Sql.Context.Hotels.Repositories;

public class RoomRepository : RepositoryBase<Room, RoomModel>,
    IRoomRepository
{
    public RoomRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}