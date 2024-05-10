using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Abstractions.Notifications;
using Chat.Domain.Entities;
using Chat.Domain.ErrorMessages;
using Chat.Domain.Repositories;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandHandler(
    IUserContext _userContext,
    IMessageNotificationService _notificationService,
    IItemService _itemService,
    IMessageRepository _messageRepository,
    IConversationRepository _conversationRepository) 
    : ICommandHandler<UpdateMessageCommand, Message>
{
    public async Task<Message> Handle(
        UpdateMessageCommand command, 
        CancellationToken cancellationToken)
    {
        var messageId = command.MessageId;
        var request = command.Request;

        var message = await _messageRepository.GetByIdAsync(messageId, cancellationToken);
        NotFoundException.ThrowIfNull(message);

        if (message.SenderId != _userContext.UserId)
        {
            throw new ForbiddenException(MessageErrorMessages.UpdateAlienMessage);
        }

        var conversation = await _conversationRepository.GetByIdAsync(message.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var item = await _itemService.GetByIdAsync(conversation.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        if (item.IsActive == false)
        {
            throw new ConflictException(MessageErrorMessages.InactiveItem);
        }

        message.Text = request.Text;

        await _messageRepository.UpdateAsync(message, cancellationToken);

        await _notificationService.UpdateMessageAsync(message, cancellationToken);

        return message;
    }
}