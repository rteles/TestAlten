using Alten.Booking.Core.Mediator.Interfaces;
using Alten.Booking.Core.Messages;
using Alten.Booking.Infra.Data.Sql.Base.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Alten.Booking.Infra.Data.Sql.Base;

public abstract class ReadOnlyRepositoryBase<TDomainEntity, TEntityModel> :
    IReadOnlyRepositoryBase<TDomainEntity>
    where TDomainEntity : class
    where TEntityModel : class
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;

    protected ReadOnlyRepositoryBase(DbContext context, IMapper mapper, IMediatorHandler mediatorHandler)
    {
        _context = context;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }
    
    public virtual async Task<TDomainEntity?> GetById(object id, CancellationToken cancellationToken = default) =>
        _mapper.Map<TDomainEntity>(await _context.Set<TEntityModel>()
            .FindAsync(new[] { id }, cancellationToken: cancellationToken));

    public virtual async Task<bool> IsDatabaseConnected(CancellationToken cancellationToken = default)
    {
        if (await _context.Database.CanConnectAsync(cancellationToken)) return true;
        await _mediatorHandler.SendNotification(cancellationToken, Notification.Create("Database not connected!"));
        return false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}