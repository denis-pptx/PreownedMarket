using Chat.Domain.Entities;
using Chat.Infrastructure.Models;

namespace Chat.Infrastructure.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(MessageNotificationModel messageNotification);
    Task UpdateMessage(MessageNotificationModel messageNotification);
    Task DeleteMessage(Guid messageId);
}