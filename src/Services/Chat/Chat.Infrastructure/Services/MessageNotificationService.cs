using Chat.Application.Abstractions;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Hubs;
using Identity.Application.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Services;

public class MessageNotificationService(
    IHubContext<ChatHub, IChatHub> _hubContext,
    IConversationRepository _conversationRepository) 
    : IMessageNotificationService
{
    public async Task SendMessageAsync(Message message)
    {
        var userIds = await GetConversationUserIds(message.ConversationId);

        await _hubContext.Clients.Users(userIds).ReceiveMessage(message);
    }

    public async Task UpdateMessageAsync(Message message)
    {
        var userIds = await GetConversationUserIds(message.ConversationId);

        await _hubContext.Clients.Users(userIds).UpdateMessage(message);
    }

    public async Task DeleteMessageAsync(Message message)
    {
        var userIds = await GetConversationUserIds(message.ConversationId);

        await _hubContext.Clients.Users(userIds).DeleteMessage(message);
    }

    private async Task<IEnumerable<string>> GetConversationUserIds(string conversationId)
    {
        var conversation = await _conversationRepository.GetByIdAsync(conversationId);
        NotFoundException.ThrowIfNull(conversation);

        var userIds = conversation.Members.Select(x => x.Id);

        return userIds;
    }
}