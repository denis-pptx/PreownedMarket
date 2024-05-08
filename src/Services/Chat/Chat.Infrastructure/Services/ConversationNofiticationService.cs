using Chat.Application.Abstractions.Notifications;
using Chat.Domain.Entities;
using Chat.Infrastructure.Hubs;
using Chat.Infrastructure.Models.Conversations;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Services;

public class ConversationNofiticationService(IHubContext<ChatHub, IChatHub> _hubContext)
    : IConversationNotificationService
{
    public async Task CreateConversationAsync(Conversation conversation, CancellationToken token = default)
    {
        var receiversIds = GetReceiversIds(conversation);

        var notification = new ConversationCreatedNotification(conversation.Id);

        await _hubContext.Clients
            .Users(receiversIds)
            .CreateConversation(notification);
    }

    public async Task DeleteConversationAsync(Conversation conversation, CancellationToken token = default)
    {
        var receiversIds = GetReceiversIds(conversation);

        var notification = new ConversationDeletedNotification(conversation.Id);

        await _hubContext.Clients
            .Users(receiversIds)
            .DeleteConversation(notification);
    }

    private static IEnumerable<string> GetReceiversIds(Conversation conversation) => 
        conversation.MembersIds.Select(guid => guid.ToString());
}