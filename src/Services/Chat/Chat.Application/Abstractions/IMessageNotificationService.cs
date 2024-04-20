using Chat.Domain.Entities;

namespace Chat.Application.Abstractions;

public interface IMessageNotificationService
{
    Task SendMessageAsync(Message message, CancellationToken token = default);
    Task UpdateMessageAsync(Message message, CancellationToken token = default);
    Task DeleteMessageAsync(Message message, CancellationToken token = default);
}