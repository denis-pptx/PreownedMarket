using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Application.Models.DataTransferObjects.Messages.Requests;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;
using MediatR;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandHandler(
    ICurrentUserService _userService,
    IMessageNotificationService _notificationService,
    IMessageRepository _messageRepository) 
    : ICommandHandler<UpdateMessageCommand, UpdateMessageResponse>
{
    public async Task<UpdateMessageResponse> Handle(
        UpdateMessageCommand command, 
        CancellationToken cancellationToken)
    {
        var messageId = command.MessageId;
        var request = command.Request;

        var message = await _messageRepository.GetByIdAsync(messageId, cancellationToken);
        NotFoundException.ThrowIfNull(message);

        var userId = _userService.UserId;
        UnauthorizedException.ThrowIfNull(userId);

        if (message.SenderId != userId)
        {
            throw new ForbiddenException(MessageErrorMessages.UpdateAlienMessage);
        }

        message.Text = request.Text;

        await _messageRepository.UpdateAsync(message, cancellationToken);

        await _notificationService.UpdateMessageAsync(message, cancellationToken);
        
        return new UpdateMessageResponse(
            message.Id, 
            message.CreatedAt, 
            message.SenderId, message.
            ConversationId);
    }
}