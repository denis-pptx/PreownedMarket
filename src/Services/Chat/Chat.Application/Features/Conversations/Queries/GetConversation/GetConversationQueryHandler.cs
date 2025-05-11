using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Conversations.Responses;
using Chat.Domain.ErrorMessages;
using Chat.Domain.Repositories;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetConversation;

public class GetConversationQueryHandler(
    IUserContext userContext,
    IItemService itemService,
    IConversationRepository conversationRepository,
    IMessageRepository messageRepository,
    IUserRepository userRepository)
    : IQueryHandler<GetConversationQuery, GetConversationResponse>
{
    public async Task<GetConversationResponse> Handle(
        GetConversationQuery query, 
        CancellationToken cancellationToken)
    {
        var conversation = await conversationRepository.GetByIdAsync(query.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var userId = userContext.UserId;

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        if (!conversation.MembersIds.Contains(user.Id)) 
        {
            throw new ForbiddenException(ConversationErrorMessages.AlienConversation);
        }

        var item = await itemService.GetByIdAsync(conversation.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var members = await userRepository.GetMembersByConversationIdAsync(conversation.Id, cancellationToken);

        var messages = await messageRepository.GetByConversationIdAsync(conversation.Id, cancellationToken);

        return new GetConversationResponse(
            conversation.Id,
            item,
            members,
            messages);
    }
}