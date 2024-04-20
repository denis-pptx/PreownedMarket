using Chat.Application.Abstractions;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Hubs;
using Chat.Infrastructure.Models;
using Identity.Application.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Services;

public class MessageNotificationService(
    IHubContext<ChatHub, IChatHub> _hubContext,
    IConversationRepository _conversationRepository) 
    : IMessageNotificationService
{
    public async Task SendMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        var messageNotification = new MessageNotificationModel(
            message.Id, 
            message.CreatedAt, 
            message.SenderId, 
            message.ConversationId);

        await _hubContext.Clients.Users(receiversIds).ReceiveMessage(messageNotification);
    }

    public async Task UpdateMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        var messageNotification = new MessageNotificationModel(
            message.Id,
            message.CreatedAt,
            message.SenderId,
            message.ConversationId);

        await _hubContext.Clients.Users(receiversIds).UpdateMessage(messageNotification);
    }

    public async Task DeleteMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        await _hubContext.Clients.Users(receiversIds).DeleteMessage(message.Id);
    }

    private async Task<IEnumerable<string>> GetReceiversIdsAsync(Message message, CancellationToken token = default)
    {
        var conversation = await _conversationRepository.GetByIdAsync(message.ConversationId, token);
        NotFoundException.ThrowIfNull(conversation);

        var membersIds = conversation.Members.Select(x => x.Id);

        var receiversIds = membersIds.Where(id => id != message.SenderId);

        return receiversIds.Select(x => x.ToString());
    }
}