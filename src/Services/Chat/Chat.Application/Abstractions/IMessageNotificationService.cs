using Chat.Domain.Entities;

namespace Chat.Application.Abstractions;

public interface IMessageNotificationService
{
    Task SendMessageAsync(Message message);
    Task UpdateMessageAsync(Message message);
}