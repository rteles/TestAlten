using Alten.Booking.Domain.Base;

namespace Alten.Booking.Infra.Data.Sql.Base.Interfaces;

public interface IReadOnlyRepositoryBase<TEntity> : IReadOnlyRepositoryDomainBase<TEntity>
    where TEntity : class
{
    Task<bool> IsDatabaseConnected(CancellationToken cancellationToken = default);
}