using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MongoDB.Driver;

namespace Chat.Infrastructure.Repositories;

public class ItemRepository(IApplicationDbContext dbContext)
    : MongoRepository<Item>(dbContext), IItemRepository
{
    public async Task DeleteByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        await _collection.DeleteManyAsync(item => item.UserId == userId, token);
    }
}