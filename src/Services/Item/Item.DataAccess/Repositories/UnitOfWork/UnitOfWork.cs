
using Item.DataAccess.Data;

namespace Item.DataAccess.Repositories.UnitOfWork;

public class UnitOfWork(ApplicationDbContext _dbContext) 
    : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}