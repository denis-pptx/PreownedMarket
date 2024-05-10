using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Common;
using Item.DataAccess.Specifications.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

using Item = Models.Entities.Item;

public class LikeRepository(ApplicationDbContext dbContext)
    : BaseRepository<Like>(dbContext), ILikeRepository
{
    public Task<Like?> GetByItemAndUserAsync(Guid itemId, Guid userId, CancellationToken cancellationToken = default)
    {
        return _entities.SingleOrDefaultAsync(
            like => 
                like.ItemId == itemId && 
                like.UserId == userId, 
            cancellationToken);
    }

    public async Task<IEnumerable<Item>> GetLikedByUserItemsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var itemsQuery = from like in _dbContext.Likes
                         join item in _dbContext.Items
                         on like.ItemId equals item.Id
                         where like.UserId == userId
                         orderby like.CreatedOn
                         select item;

        var items = await itemsQuery
            .ApplySpecification(new ItemSpecification())
            .ToListAsync(cancellationToken);

        return items;
    }
}