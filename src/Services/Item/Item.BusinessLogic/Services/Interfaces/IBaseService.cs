using Item.DataAccess.Models;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IBaseService<TEntity, TEntityDto>
    where TEntity : BaseEntity
{
    Task<TEntity> CreateAsync(TEntityDto entityDto, CancellationToken token = default);
    Task<IEnumerable<TEntity>> GetAsync(CancellationToken token = default);
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<TEntity> UpdateAsync(Guid id, TEntityDto entityDto, CancellationToken token = default);
    Task<TEntity> DeleteByIdAsync(Guid id, CancellationToken token = default);
}