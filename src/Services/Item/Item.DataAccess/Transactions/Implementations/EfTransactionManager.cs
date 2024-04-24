using Item.DataAccess.Data;
using Item.DataAccess.Transactions.Interfaces;

namespace Item.DataAccess.Transactions.Implementations;

public class EfTransactionManager(ApplicationDbContext _dbContext)
    : ITransactionManager
{
    public async Task<ITransaction> BeginTransactionAsync(CancellationToken token = default)
    {
        var _transaction = await _dbContext.Database.BeginTransactionAsync(token);

        return new EfTransaction(_transaction);
    }
}
