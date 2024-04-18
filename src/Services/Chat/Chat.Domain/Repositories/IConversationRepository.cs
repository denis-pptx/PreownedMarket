using Chat.Domain.Entities;
using System.Linq.Expressions;

namespace Chat.Domain.Repositories;

public interface IConversationRepository
{
    Task<IEnumerable<Conversation>> GetByUserIdAsync(string userId, CancellationToken token = default);
    Task<Conversation?> GetByIdAsync(string id, CancellationToken token = default);
    Task<Conversation?> FirstOrDefaultAsync(Expression<Func<Conversation, bool>> filter, CancellationToken token = default);
}