using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Chat.Infrastructure.Repositories;

public class ConversationRepository(IApplicationDbContext dbContext)
    : MongoRepository<Conversation>(dbContext), IConversationRepository
{
    public async Task<IEnumerable<Conversation>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        return await _collection
            .Find(conversation => conversation.MembersIds.Contains(userId))
            .ToListAsync(token);
    }

    public async Task<Conversation?> FirstOrDefaultAsync(Expression<Func<Conversation, bool>> filter, CancellationToken token = default)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync(token);
    }

    public async Task DeleteByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        await _collection.DeleteManyAsync(
            conversation => conversation.MembersIds.Contains(userId), 
            token);
    }

    public async Task DeleteByItemIdAsync(Guid itemId, CancellationToken token = default)
    {
        await _collection.DeleteManyAsync(
            conversation => conversation.ItemId == itemId,
            token);
    }

    public async Task<IEnumerable<Guid>> GetConversationsIdsByItemIdAsync(Guid itemId, CancellationToken token = default)
    {
        return await _collection
            .Find(conversation => conversation.ItemId == itemId)
            .Project(conversation => conversation.Id)
            .ToListAsync(token);
    }
}