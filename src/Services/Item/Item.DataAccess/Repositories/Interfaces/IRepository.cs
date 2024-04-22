using Item.DataAccess.Models.Entities;
using Item.DataAccess.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Item.DataAccess.Repositories.Interfaces;

public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<IEnumerable<TEntity>> GetAsync(CancellationToken token = default);
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default);
    Task DeleteAsync(TEntity entity, CancellationToken token = default);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default);
    Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken token = default);
    Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification, CancellationToken token = default);
}