using AutoMapper;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Abstractions.Notifications;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandHandler(
    IUserContext _userContext,
    IMessageNotificationService _notificationService,
    IMessageRepository _messageRepository) 
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

        message.Text = request.Text;

        await _messageRepository.UpdateAsync(message, cancellationToken);

        await _notificationService.UpdateMessageAsync(message, cancellationToken);

        return message;
    }
}