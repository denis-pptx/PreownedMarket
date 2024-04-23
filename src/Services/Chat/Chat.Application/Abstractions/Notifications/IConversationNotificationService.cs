using Chat.Domain.Entities;

namespace Chat.Application.Abstractions.Notifications;

public interface IConversationNotificationService
{
    Task CreateConversationAsync(Conversation conversation, CancellationToken token = default);
    Task DeleteConversationAsync(Conversation conversation, CancellationToken token = default);
}