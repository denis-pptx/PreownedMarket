using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Abstractions.Notifications;
using Chat.Domain.Entities;
using Chat.Domain.ErrorMessages;
using Chat.Domain.Repositories;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Messages.Commands.CreateMessage;

public class CreateMessageCommandHandler(
    IUserContext userContext,
    IItemService itemService,
    IUserRepository userRepository,
    IMessageRepository messageRepository,
    IConversationRepository conversationRepository,
    IMessageNotificationService notificationService)
    : ICommandHandler<CreateMessageCommand, Message>
{
    public async Task<Message> Handle(
        CreateMessageCommand command,
        CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var sender = await userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(sender);

        var request = command.Request;

        var conversation = await conversationRepository.GetByIdAsync(request.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var item = await itemService.GetByIdAsync(conversation.ItemId, cancellationToken);
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

        await messageRepository.AddAsync(message, cancellationToken);

        await notificationService.SendMessageAsync(message, cancellationToken);

        return message;
    }
}