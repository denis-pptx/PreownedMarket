using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetUserConversations;

public class GetUserConversationsHandler(
    ICurrentUserService _currentUserService,
    IUserRepository _userRepository,
    IConversationRepository _conversationRepository) 
    : IQueryHandler<GetUserConversationsQuery, IEnumerable<Conversation>>
{
    public async Task<IEnumerable<Conversation>> Handle(
        GetUserConversationsQuery query, 
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        UnauthorizedException.ThrowIfNull(userId);

        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(user);

        var conversations = await _conversationRepository.GetByUserIdAsync(userId, cancellationToken);

        return conversations;
    }
}