using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Common;
using Item.DataAccess.Specifications.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

public class RegionRepository(ApplicationDbContext dbContext) 
    : BaseRepository<Region>(dbContext), IRegionRepository
{
    public override async Task<Region?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _entities
            .ApplySpecification(new RegionSpecification(id))
            .SingleOrDefaultAsync(entity => entity.Id == id, token);
    }

    public override async Task<IEnumerable<Region>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entities
            .ApplySpecification(new RegionSpecification())
            .ToListAsync(cancellationToken);
    }

    public async Task<Region?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _entities
            .ApplySpecification(new RegionSpecification())
            .SingleOrDefaultAsync(cancellationToken);
    }
}