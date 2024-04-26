using Chat.Domain.Entities;

namespace Chat.Domain.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetByConversationIdAsync(Guid conversationId, CancellationToken token = default);
    Task AddAsync(Message message, CancellationToken token = default);
    Task<Message?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task UpdateAsync(Message message, CancellationToken token = default);
    Task DeleteAsync(Message message, CancellationToken token = default);
    Task DeleteByConversationIdAsync(Guid conversationId, CancellationToken token = default);
    Task<Message?> GetLastMessageInConversationAsync(Guid conversationId, CancellationToken token = default);
    Task DeleteByUserIdAsync(Guid userId, CancellationToken token = default);
}