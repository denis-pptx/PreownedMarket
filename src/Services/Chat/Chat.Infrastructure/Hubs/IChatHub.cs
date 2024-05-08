using Chat.Domain.Entities;
using Chat.Infrastructure.Models.Conversations;

namespace Chat.Infrastructure.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(Message message);
    Task UpdateMessage(Message message);
    Task DeleteMessage(Message message);

    Task CreateConversation(ConversationCreatedNotification notification);
    Task DeleteConversation(ConversationDeletedNotification notification);
}