using Chat.Application.Data;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

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

    public async Task<Conversation?> FirstOrDefaultAsync(Expression<Func<Conversation, bool>> filter, CancellationToken token = default)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync(token);
    }
}