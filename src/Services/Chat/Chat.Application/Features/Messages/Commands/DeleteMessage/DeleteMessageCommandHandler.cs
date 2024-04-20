using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Messages.Commands.DeleteMessage;

public class DeleteMessageCommandHandler(
    IUserContext _userContext,
    IMessageNotificationService _notificationService,
    IMessageRepository _messageRepository) 
    : ICommandHandler<DeleteMessageCommand, DeleteMessageResponse>
{
    public async Task<DeleteMessageResponse> Handle(
        DeleteMessageCommand command, 
        CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(command.MessageId, cancellationToken);
        NotFoundException.ThrowIfNull(message);

        var userId = _userContext.UserId;

        if (userId != message.SenderId)
        {
            throw new ForbiddenException(MessageErrorMessages.DeleteAlienMessage);
        }

        await _messageRepository.DeleteAsync(message, cancellationToken);

        await _notificationService.DeleteMessageAsync(message, cancellationToken);

        return new DeleteMessageResponse(message.Id);
    }
}