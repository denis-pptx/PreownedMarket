namespace Item.DataAccess.Transactions.Interfaces;

public interface ITransactionManager 
{
    Task<ITransaction> BeginTransactionAsync(CancellationToken token = default);
}
