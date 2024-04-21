using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Exceptions.ErrorMessages;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;
using MediatR;

namespace Chat.Application.Features.Conversations.Commands.DeleteConversation;

public class DeleteConversationCommandHandler(
    IUserContext _userContext, 
    IConversationRepository _conversationRepository,
    IUserRepository _userRepository,
    IMessageRepository _messageRepository)
    : ICommandHandler<DeleteConversationCommand, Unit>
{
    public async Task<Unit> Handle(
        DeleteConversationCommand command, 
        CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByIdAsync(command.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var user = await _userRepository.GetByIdAsync(_userContext.UserId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        if (!conversation.Members.Contains(user))
        {
            throw new ForbiddenException(ConversationErrorMessages.AlienConversation);
        }

        await _messageRepository.DeleteByConversationIdAsync(conversation.Id, cancellationToken);

        await _conversationRepository.DeleteAsync(conversation, cancellationToken);

        return Unit.Value;
    }
}
