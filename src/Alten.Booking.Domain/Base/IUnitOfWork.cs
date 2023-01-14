namespace Alten.Booking.Domain.Base;

public interface IUnitOfWork : IDisposable
{
    // Task BeginTransaction();
    Task<bool> Commit();
    // Task RollbackTransaction();
}