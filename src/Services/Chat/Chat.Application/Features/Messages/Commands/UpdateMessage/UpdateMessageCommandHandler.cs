using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;
using MediatR;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandHandler(
    ICurrentUserService _userService,
    IMessageRepository _messageRepository
    ) 
    : ICommandHandler<UpdateMessageCommand, Unit>
{
    public async Task<Unit> Handle(
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
        
        return Unit.Value;
    }
}