using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

public class ImageRepository(ApplicationDbContext dbContext)
    : BaseRepository<ItemImage>(dbContext), IImageRepository
{
    public async Task<IEnumerable<ItemImage>> GetByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        return await _entities
            .Where(image => image.ItemId == itemId)
            .ToListAsync(cancellationToken);
    }
}