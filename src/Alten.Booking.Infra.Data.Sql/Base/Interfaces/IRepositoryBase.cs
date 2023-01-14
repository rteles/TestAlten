using Alten.Booking.Domain.Base;

namespace Alten.Booking.Infra.Data.Sql.Base.Interfaces;

public interface IRepositoryBase<TDomainEntity> : IRepositoryDomainBase<TDomainEntity>
    where TDomainEntity : class
{
}