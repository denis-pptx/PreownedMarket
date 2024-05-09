using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

public abstract class BaseRepository<TEntity>(ApplicationDbContext dbContext)
    where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _dbContext = dbContext;
    protected readonly DbSet<TEntity> _entities = dbContext.Set<TEntity>();

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _entities.SingleOrDefaultAsync(
            entity => entity.Id == id,
            cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(
            () => _entities.Add(entity), 
            cancellationToken);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(
            () => _entities.Update(entity), 
            cancellationToken);
    }

    public virtual async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(
            () => _entities.Remove(entity), 
            cancellationToken);
    }
}