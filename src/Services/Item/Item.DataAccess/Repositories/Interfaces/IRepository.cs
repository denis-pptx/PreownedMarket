using Item.DataAccess.Models;
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
}
