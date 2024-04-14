using Item.DataAccess.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Item.DataAccess.Transactions.Implementations;

public class EfTransaction(IDbContextTransaction _transaction) 
    : ITransaction
{
    public async Task CommitAsync(CancellationToken token = default)
    {
        await _transaction.CommitAsync(token);
    }

    public async Task RollbackAsync(CancellationToken token = default)
    {
        await _transaction.RollbackAsync(token);
    }

    public void Dispose()
    {
        _transaction.Dispose();
    }
}
