using Chat.Domain.Entities;

namespace Chat.Domain.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetByConversationIdAsync(string conversationId, CancellationToken token = default);
    Task AddAsync(Message message, CancellationToken token = default);
    Task<Message?> GetByIdAsync(string id, CancellationToken token = default);
    Task UpdateAsync(Message message, CancellationToken token = default);
    Task DeleteAsync(Message message, CancellationToken token = default);
}