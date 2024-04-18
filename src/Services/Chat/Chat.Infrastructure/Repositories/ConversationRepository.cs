using Chat.Application.Data;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MongoDB.Driver;

namespace Chat.Infrastructure.Repositories;

public class ConversationRepository(IApplicationDbContext dbContext)
    : MongoRepository<Conversation>(dbContext), IConversationRepository
{
    public async Task<IEnumerable<Conversation>> GetByUserIdAsync(string userId, CancellationToken token = default)
    {
        return await _collection
            .Find(conversation => conversation.Members.Any(member => member.Id == userId))
            .ToListAsync(token);
    }
}