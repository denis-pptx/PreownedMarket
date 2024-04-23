using AutoMapper;
using Chat.Application.Abstractions.Notifications;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Chat.Infrastructure.Hubs;
using Chat.Infrastructure.Models.Messages;
using Identity.Application.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Services;

public class MessageNotificationService(
    IMapper _mapper,
    IHubContext<ChatHub, IChatHub> _hubContext,
    IConversationRepository _conversationRepository) 
    : IMessageNotificationService
{
    public async Task SendMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        var messageNotification = _mapper.Map<Message, MessageNotification>(message);

        await _hubContext.Clients.Users(receiversIds).ReceiveMessage(messageNotification);
    }

    public async Task UpdateMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        var messageNotification = _mapper.Map<Message, MessageNotification>(message);

        await _hubContext.Clients.Users(receiversIds).UpdateMessage(messageNotification);
    }

    public async Task DeleteMessageAsync(Message message, CancellationToken token = default)
    {
        var receiversIds = await GetReceiversIdsAsync(message, token);

        var messageNotification = _mapper.Map<Message, MessageNotification>(message);

        await _hubContext.Clients.Users(receiversIds).DeleteMessage(messageNotification);
    }

    private async Task<IEnumerable<string>> GetReceiversIdsAsync(Message message, CancellationToken token = default)
    {
        var conversation = await _conversationRepository.GetByIdAsync(message.ConversationId, token);
        NotFoundException.ThrowIfNull(conversation);

        var membersIds = conversation.Members.Select(x => x.Id);

        return membersIds.Select(x => x.ToString());
    }
}