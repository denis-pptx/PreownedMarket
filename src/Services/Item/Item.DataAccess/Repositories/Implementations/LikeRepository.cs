using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;


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
}