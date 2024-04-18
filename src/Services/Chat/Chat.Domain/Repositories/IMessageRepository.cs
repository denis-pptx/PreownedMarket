using Chat.Domain.Entities;

namespace Chat.Domain.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetByConversationIdAsync(string conversationId, CancellationToken token = default);
}