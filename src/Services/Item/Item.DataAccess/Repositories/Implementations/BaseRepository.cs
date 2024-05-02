using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

public abstract class BaseRepository<TEntity>(ApplicationDbContext _dbContext)
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> _entities = _dbContext.Set<TEntity>();

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

    public virtual void Add(TEntity entity)
    {
        _entities.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _entities.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }
}