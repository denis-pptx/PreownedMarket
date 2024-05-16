using Item.DataAccess.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Item.DataAccess.UnitOfWork;

public class EfUnitOfWork(ApplicationDbContext _dbContext)
    : IUnitOfWork
{
    public IDbTransaction BeginTransaction()
    {
        var transaction = _dbContext
            .Database
            .BeginTransaction();

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}