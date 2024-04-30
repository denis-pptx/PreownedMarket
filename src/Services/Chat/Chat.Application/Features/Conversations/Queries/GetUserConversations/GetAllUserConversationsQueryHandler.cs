using AutoMapper;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetUserConversations;

public class GetAllUserConversationsQueryHandler(
    IUserContext _userContext,
    IItemService _itemService,
    IUserRepository _userRepository,
    IConversationRepository _conversationRepository,
    IMessageRepository _messageRepository) 
    : IQueryHandler<GetAllUserConversationsQuery, IEnumerable<GetConversationWithLastMessageResponse>>
{
    public async Task<IEnumerable<GetConversationWithLastMessageResponse>> Handle(
        GetAllUserConversationsQuery query, 
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        var conversations = await _conversationRepository.GetByUserIdAsync(userId, cancellationToken);

        var response = conversations.Select(async conversation =>
        {
            var item = await _itemService.GetByIdAsync(conversation.ItemId, cancellationToken);
            NotFoundException.ThrowIfNull(item);

            var lastMessage = await _messageRepository.GetLastMessageInConversationAsync(conversation.Id, cancellationToken);

            var members = await _userRepository.GetMembersByConversationIdAsync(conversation.Id, cancellationToken);

            return new GetConversationWithLastMessageResponse(
                conversation.Id,
                item, 
                lastMessage,
                members);
        });

        return await Task.WhenAll(response);
    }
}