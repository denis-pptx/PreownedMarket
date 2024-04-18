using Chat.Application.Abstractions;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Hubs;
using Identity.Application.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Services;

public class MessageService(
    IHubContext<ChatHub, IChatHub> _hubContext,
    IConversationRepository _conversationRepository) 
    : IMessageService
{
    public async Task SendMessageAsync(Message message)
    {
        var conversation = await _conversationRepository.GetByIdAsync(message.ConversationId);
        NotFoundException.ThrowIfNull(conversation);

        var userIds = conversation.Members.Select(x => x.Id);

        await _hubContext.Clients.Users(userIds).ReceiveMessage(message);
    }
}