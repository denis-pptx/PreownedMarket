using Chat.Domain.Entities;

namespace Chat.Application.Abstractions;

public interface IMessageService
{
    Task SendMessageAsync(Message message);
}
