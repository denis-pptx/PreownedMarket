using Chat.Domain.Entities;
using Chat.Infrastructure.Models.Conversations;
using Chat.Infrastructure.Models.Messages;

namespace Chat.Infrastructure.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(MessageNotification messageNotification);
    Task UpdateMessage(MessageNotification messageNotification);
    Task DeleteMessage(MessageNotification messageNotification);

    Task CreateConversation(ConversationCreatedNotification notification);
    Task DeleteConversation(ConversationDeletedNotification notification);
}