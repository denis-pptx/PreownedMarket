using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface IStatusRepository
{
    Task<IEnumerable<Status>> GetAllAsync(CancellationToken cancellationToken = default); 
    Task<Status?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Status> GetByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken = default);
    Task<Status> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default);
}