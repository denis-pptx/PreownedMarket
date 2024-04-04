using Item.DataAccess.Data;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Common;
using Item.DataAccess.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Item.DataAccess.Repositories.Implementations;

public class EfRepository<TEntity>(ApplicationDbContext dbContext)
    : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _dbContext = dbContext;
    protected readonly DbSet<TEntity> _entities = dbContext.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default)
    {
        await _entities.AddAsync(entity, token);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken token = default)
    {
        _entities.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
    {
        return await _entities.FirstOrDefaultAsync(filter, token);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken token = default)
    {
        return await _entities.AsQueryable()
            .ApplySpecification(specification)
            .FirstOrDefaultAsync(token);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken token = default)
    {
        return await _entities.ToListAsync(token);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
    {
        return await _entities.Where(filter).ToListAsync(token);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken token = default)
    {
        return await _entities.AsQueryable()
            .ApplySpecification(specification)
            .ToListAsync(token);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _entities.SingleAsync(e => e.Id.Equals(id), token);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
    {
        return await _entities.SingleOrDefaultAsync(filter, token);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token)
    {
        _entities.Update(entity);
        await _dbContext.SaveChangesAsync(token);

        return entity;
    }
}
