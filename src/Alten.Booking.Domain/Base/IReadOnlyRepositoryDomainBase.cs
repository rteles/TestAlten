namespace Alten.Booking.Domain.Base;

public interface IReadOnlyRepositoryDomainBase<TEntity> : IDisposable
    where TEntity : class
{
    Task<TEntity?> GetById(object id, CancellationToken cancellationToken = default);
}