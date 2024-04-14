using Item.DataAccess.Data;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Common;
using Item.DataAccess.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

using Item = Models.Item;

public class LikeRepository(ApplicationDbContext dbContext) :
    EfRepository<Like>(dbContext), ILikeRepository
{
    public async Task<IEnumerable<Item>> GetByUserIdAsync(
        Guid userId,
        ISpecification<Item> itemSpecification,
        CancellationToken token = default)
    {
        var likedItems = from like in _dbContext.Likes
                         join item in _dbContext.Items on like.ItemId equals item.Id
                         where like.UserId == userId
                         orderby like.CreatedOn descending
                         select item;

        var items = await likedItems
            .ApplySpecification(itemSpecification)
            .ToListAsync(token);

        return items;
    }
}