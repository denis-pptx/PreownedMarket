using Chat.Domain.Entities;

namespace Chat.Infrastructure.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(Message message);
    Task UpdateMessage(Message message);
    Task DeleteMessage(Message message);
}