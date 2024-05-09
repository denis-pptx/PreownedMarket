using Chat.Application.Abstractions.Notifications;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Shared.Errors.Exceptions;

namespace Chat.Infrastructure.Services;

public class MessageNotificationService(
    IHubContext<ChatHub, IChatHub> _hubContext,
    IConversationRepository _conversationRepository) 
    : IMessageNotificationService
{
    public async Task SendMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        await _hubContext.Clients.Users(receiversIds).ReceiveMessage(message);
    }

    public async Task UpdateMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        await _hubContext.Clients.Users(receiversIds).UpdateMessage(message);
    }

    public async Task DeleteMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        await _hubContext.Clients.Users(receiversIds).DeleteMessage(message);
    }

    private async Task<IEnumerable<string>> GetReceiversIdsAsync(Message message, CancellationToken token = default)
    {
        var conversation = await _conversationRepository.GetByIdAsync(message.ConversationId, token);
        NotFoundException.ThrowIfNull(conversation);

        return conversation
            .MembersIds
            .Select(guid => guid.ToString());
    }
}