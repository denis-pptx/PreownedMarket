using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;
using MediatR;

namespace Chat.Application.Features.Messages.Commands.CreateMessage;

public class CreateMessageCommandHandler(
    ICurrentUserService _currentUserService,
    IUserRepository _userRepository,
    IMessageRepository _messageRepository,
    IConversationRepository _conversationRepository,
    IMessageNotificationService _messageService)
    : ICommandHandler<CreateMessageCommand, Unit>
{
    public async Task<Unit> Handle(
        CreateMessageCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        UnauthorizedException.ThrowIfNull(userId);

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

        await _messageService.SendMessageAsync(message);

        return Unit.Value;
    }
}