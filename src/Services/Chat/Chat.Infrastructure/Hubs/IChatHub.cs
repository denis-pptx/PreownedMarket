namespace Chat.Infrastructure.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(string message);
}
