namespace Alten.Booking.Domain.Base;

public interface IRepositoryDomainBase<TEntity> : IReadOnlyRepositoryDomainBase<TEntity>, IUnitOfWork
    where TEntity : class
{
    Task Add(TEntity obj);
    void Update(TEntity obj);
    void Remove(TEntity obj);
}