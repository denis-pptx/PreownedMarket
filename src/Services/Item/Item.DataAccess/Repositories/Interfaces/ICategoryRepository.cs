using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Category region, CancellationToken cancellationToken = default);
    Task UpdateAsync(Category region, CancellationToken cancellationToken = default);
    Task RemoveAsync(Category region, CancellationToken cancellationToken = default);
}