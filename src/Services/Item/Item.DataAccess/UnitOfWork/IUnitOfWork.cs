using System.Data;

namespace Item.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BeginTransaction();
}