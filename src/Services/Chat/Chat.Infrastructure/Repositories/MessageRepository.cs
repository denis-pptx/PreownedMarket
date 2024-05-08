using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MongoDB.Driver;

namespace Chat.Infrastructure.Repositories;

public class MessageRepository(IApplicationDbContext dbContext) 
    : MongoRepository<Message>(dbContext), IMessageRepository
{
    public async Task DeleteByConversationIdAsync(Guid conversationId, CancellationToken token = default)
    {
        await _collection.DeleteManyAsync(message => message.ConversationId == conversationId, token);
    }

    public Task DeleteByItemIdAsync(Guid itemId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteByUserIdAsync(Guid userId, CancellationToken token = default)
    {
        await _collection.DeleteManyAsync(message => message.SenderId == userId, token);    
    }

    public async Task<IEnumerable<Message>> GetByConversationIdAsync(Guid conversationId, CancellationToken token = default)
    {
        return await _collection
            .Find(message => message.ConversationId == conversationId)
            .ToListAsync(token);
    }

    public async Task<Message?> GetLastMessageInConversationAsync(Guid conversationId, CancellationToken token = default)
    {
        return await _collection
            .Find(message => message.ConversationId == conversationId)
            .SortByDescending(message => message.CreatedAt)
            .FirstOrDefaultAsync(token);
    }
}