using AutoMapper;
using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandHandler(
    IMapper _mapper,
    IUserContext _userContext,
    IMessageNotificationService _notificationService,
    IMessageRepository _messageRepository) 
    : ICommandHandler<UpdateMessageCommand, MessageResponse>
{
    public async Task<MessageResponse> Handle(
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

        var messageResponse = _mapper.Map<Message, MessageResponse>(message);

        return messageResponse;
    }
}