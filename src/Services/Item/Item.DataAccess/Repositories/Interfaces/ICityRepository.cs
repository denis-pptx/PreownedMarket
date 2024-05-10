using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface ICityRepository
{
    Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<City?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(City city, CancellationToken cancellationToken = default);
    Task UpdateAsync(City city, CancellationToken cancellationToken = default);
    Task RemoveAsync(City city, CancellationToken cancellationToken = default);
}