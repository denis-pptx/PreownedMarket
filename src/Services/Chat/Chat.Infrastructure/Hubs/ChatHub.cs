using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace Chat.Infrastructure.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHub>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.User(Context.UserIdentifier!).ReceiveMessage("Привет помидор");
        await base.OnConnectedAsync();
    }

    public void SendMessage(string message)
    {
        Debug.WriteLine($"{message}");  
    }
}