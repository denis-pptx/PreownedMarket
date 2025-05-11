using Item.DataAccess.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Item.DataAccess.UnitOfWork;

public class EfUnitOfWork(ApplicationDbContext dbContext)
    : IUnitOfWork
{
    public IDbTransaction BeginTransaction()
    {
        var transaction = dbContext
            .Database
            .BeginTransaction();

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}