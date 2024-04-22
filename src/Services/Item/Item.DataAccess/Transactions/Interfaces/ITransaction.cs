namespace Item.DataAccess.Transactions.Interfaces;

public interface ITransaction : IDisposable
{
    Task CommitAsync(CancellationToken token = default);
    Task RollbackAsync(CancellationToken token = default);
}
