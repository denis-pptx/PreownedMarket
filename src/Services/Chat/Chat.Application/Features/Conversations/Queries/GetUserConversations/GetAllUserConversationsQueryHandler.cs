using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Conversations.Responses;
using Chat.Domain.Repositories;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetUserConversations;

public class GetAllUserConversationsQueryHandler(
    IUserContext userContext,
    IItemService itemService,
    IUserRepository userRepository,
    IConversationRepository conversationRepository,
    IMessageRepository messageRepository) 
    : IQueryHandler<GetAllUserConversationsQuery, IEnumerable<GetConversationWithLastMessageResponse>>
{
    public async Task<IEnumerable<GetConversationWithLastMessageResponse>> Handle(
        GetAllUserConversationsQuery query, 
        CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        var conversations = await conversationRepository.GetByUserIdAsync(userId, cancellationToken);

        var response = conversations.Select(async conversation =>
        {
            var item = await itemService.GetByIdAsync(conversation.ItemId, cancellationToken);
            NotFoundException.ThrowIfNull(item);

            var lastMessage = await messageRepository.GetLastMessageInConversationAsync(conversation.Id, cancellationToken);

            var members = await userRepository.GetMembersByConversationIdAsync(conversation.Id, cancellationToken);

            return new GetConversationWithLastMessageResponse(
                conversation.Id,
                item, 
                lastMessage,
                members);
        });

        return await Task.WhenAll(response);
    }
}