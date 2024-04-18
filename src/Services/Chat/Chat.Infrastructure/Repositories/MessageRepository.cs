using Chat.Application.Data;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MongoDB.Driver;

namespace Chat.Infrastructure.Repositories;

public class MessageRepository(IApplicationDbContext dbContext) 
    : MongoRepository<Message>(dbContext), IMessageRepository
{
    public async Task<IEnumerable<Message>> GetByConversationIdAsync(string conversationId, CancellationToken token = default)
    {
        return await _collection
            .Find(message => message.ConversationId == conversationId)
            .ToListAsync();
    }
}
