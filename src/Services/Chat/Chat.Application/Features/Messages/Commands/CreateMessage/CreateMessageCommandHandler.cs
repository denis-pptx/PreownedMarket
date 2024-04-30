using AutoMapper;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Abstractions.Notifications;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Messages.Commands.CreateMessage;

public class CreateMessageCommandHandler(
    IUserContext _userContext,
    IItemService _itemService,
    IUserRepository _userRepository,
    IMessageRepository _messageRepository,
    IConversationRepository _conversationRepository,
    IMessageNotificationService _notificationService)
    : ICommandHandler<CreateMessageCommand, Message>
{
    public async Task<Message> Handle(
        CreateMessageCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var sender = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(sender);

        var request = command.Request;

        var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var item = await _itemService.GetByIdAsync(conversation.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        if (item.IsActive is false)
        {
            throw new ConflictException(MessageErrorMessages.InactiveItem);
        }

        var message = new Message
        {
            Text = request.Text,
            CreatedAt = DateTime.UtcNow,
            SenderId = sender.Id,
            ConversationId = conversation.Id
        };

        await _messageRepository.AddAsync(message, cancellationToken);

        await _notificationService.SendMessageAsync(message, cancellationToken);

        return message;
    }
}