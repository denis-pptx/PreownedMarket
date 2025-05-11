using AutoMapper;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Abstractions.Notifications;
using Chat.Domain.Entities;
using Chat.Domain.ErrorMessages;
using Chat.Domain.Repositories;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Messages.Commands.DeleteMessage;

public class DeleteMessageCommandHandler(
    IUserContext userContext,
    IMessageNotificationService notificationService,
    IMessageRepository messageRepository) 
    : ICommandHandler<DeleteMessageCommand, Message>
{
    public async Task<Message> Handle(
        DeleteMessageCommand command, 
        CancellationToken cancellationToken)
    {
        var message = await messageRepository.GetByIdAsync(command.MessageId, cancellationToken);
        NotFoundException.ThrowIfNull(message);

        var userId = userContext.UserId;

        if (userId != message.SenderId)
        {
            throw new ForbiddenException(MessageErrorMessages.DeleteAlienMessage);
        }

        await messageRepository.DeleteAsync(message, cancellationToken);

        await notificationService.DeleteMessageAsync(message, cancellationToken);

        return message;
    }
}