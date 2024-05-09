using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Conversations.Responses;
using Chat.Domain.ErrorMessages;
using Chat.Domain.Repositories;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetConversation;

public class GetConversationQueryHandler(
    IUserContext _userContext,
    IItemService _itemService,
    IConversationRepository _conversationRepository,
    IMessageRepository _messageRepository,
    IUserRepository _userRepository)
    : IQueryHandler<GetConversationQuery, GetConversationResponse>
{
    public async Task<GetConversationResponse> Handle(
        GetConversationQuery query, 
        CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByIdAsync(query.ConversationId, cancellationToken);
        NotFoundException.ThrowIfNull(conversation);

        var userId = _userContext.UserId;

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        if (!conversation.MembersIds.Contains(user.Id)) 
        {
            throw new ForbiddenException(ConversationErrorMessages.AlienConversation);
        }

        var item = await _itemService.GetByIdAsync(conversation.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var members = await _userRepository.GetMembersByConversationIdAsync(conversation.Id, cancellationToken);

        var messages = await _messageRepository.GetByConversationIdAsync(conversation.Id, cancellationToken);

        return new GetConversationResponse(
            conversation.Id,
            item,
            members,
            messages);
    }
}