using Chat.Domain.Entities;

namespace Chat.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task AddAsync(User user, CancellationToken token = default);
    Task DeleteAsync(User user, CancellationToken token = default);
    Task<IEnumerable<User>> GetMembersByConversationIdAsync(Guid conversationId, CancellationToken token = default);
}