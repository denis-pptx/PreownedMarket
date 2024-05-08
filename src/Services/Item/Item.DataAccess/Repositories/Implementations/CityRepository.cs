using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

public class CityRepository(ApplicationDbContext dbContext)
    : BaseRepository<City>(dbContext), ICityRepository
{
    public async Task<City?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _entities
            .Where(city => city.Name == name)
            .SingleOrDefaultAsync(cancellationToken);
    }
}