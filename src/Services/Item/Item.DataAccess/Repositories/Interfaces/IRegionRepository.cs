using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface IRegionRepository
{
    Task<Region?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Region?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Region>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Region region);
    void Update(Region region);
    void Remove(Region region);
}