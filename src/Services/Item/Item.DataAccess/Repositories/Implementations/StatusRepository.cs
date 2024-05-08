using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

public class StatusRepository(ApplicationDbContext dbContext)
    : BaseRepository<Status>(dbContext), IStatusRepository
{
    public async Task<Status> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        var item = await _dbContext.Items
            .Where(item => item.Id == itemId)
            .SingleAsync(cancellationToken);

        var status = await _dbContext.Statuses
            .Where(status => status.Id == item.StatusId)
            .SingleAsync(cancellationToken);

        return status;
    }

    public async Task<Status> GetByNormalizedNameAsync(string normalizedName, CancellationToken cancellationToken = default)
    {
        return await _entities
            .Where(status => status.NormalizedName == normalizedName)
            .SingleAsync(cancellationToken);
    }
}