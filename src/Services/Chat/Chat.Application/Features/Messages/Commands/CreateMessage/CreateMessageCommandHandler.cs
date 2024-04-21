using AutoMapper;
using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Messages.Commands.CreateMessage;

public class CreateMessageCommandHandler(
    IUserContext _userContext,
    IUserRepository _userRepository,
    IMessageRepository _messageRepository,
    IConversationRepository _conversationRepository,
    IMessageNotificationService _notificationService,
    IMapper _mapper)
    : ICommandHandler<CreateMessageCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(
        CreateMessageCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var sender = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(sender);

        var request = command.Request;

        var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var message = new Message
        {
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            SenderId = sender.Id,
            ConversationId = conversation.Id
        };

        await _messageRepository.AddAsync(message, cancellationToken);

        await _notificationService.SendMessageAsync(message, cancellationToken);

        var messageResponse = _mapper.Map<Message, MessageResponse>(message);

        return messageResponse;
    }
}