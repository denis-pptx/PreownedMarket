using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Abstractions.Notifications;
using Chat.Domain.ErrorMessages;
using Chat.Domain.Repositories;
using MediatR;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Conversations.Commands.DeleteConversation;

public class DeleteConversationCommandHandler(
    IUserContext userContext,
    IConversationNotificationService notificationService,
    IConversationRepository conversationRepository,
    IUserRepository userRepository,
    IMessageRepository messageRepository)
    : ICommandHandler<DeleteConversationCommand, Unit>
{
    public async Task<Unit> Handle(
        DeleteConversationCommand command, 
        CancellationToken cancellationToken)
    {
        var conversation = await conversationRepository.GetByIdAsync(command.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var user = await userRepository.GetByIdAsync(userContext.UserId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        if (!conversation.MembersIds.Contains(user.Id))
        {
            throw new ForbiddenException(ConversationErrorMessages.AlienConversation);
        }

        await messageRepository.DeleteByConversationIdAsync(conversation.Id, cancellationToken);

        await conversationRepository.DeleteAsync(conversation, cancellationToken);

        await notificationService.DeleteConversationAsync(conversation, cancellationToken);

        return Unit.Value;
    }
}