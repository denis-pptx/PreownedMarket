using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace Chat.Infrastructure.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHub>
{

}