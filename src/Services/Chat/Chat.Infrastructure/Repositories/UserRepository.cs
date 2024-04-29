using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MongoDB.Driver;

namespace Chat.Infrastructure.Repositories;

public class UserRepository(IApplicationDbContext dbContext)
    : MongoRepository<User>(dbContext), IUserRepository
{
    public async Task<IEnumerable<User>> GetMembersByConversationIdAsync(Guid conversationId, CancellationToken token = default)
    {
        var membersIds = await _dbContext.Conversations
            .Find(conversation => conversation.Id == conversationId)
            .Project(conversation => conversation.MembersIds)
            .SingleAsync(token);

        var members = await _collection
            .Find(user => membersIds.Contains(user.Id))
            .ToListAsync(token);

        return members;
    }
}