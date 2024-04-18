using Chat.Application.Abstractions;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.CheckConversationExistence;

public class CheckConversationExistenceHandler(
    ICurrentUserService _currentUserService,
    IUserRepository _userRepository,
    IItemRepository _itemRepository,
    IConversationRepository _conversationRepository)
    : ICommandHandler<CheckConversationExistenceQuery, bool>
{
    public async Task<bool> Handle(
        CheckConversationExistenceQuery query,
        CancellationToken cancellationToken)
    {
        var request = query.Request;

        var currentUserId = _currentUserService.UserId;
        UnauthorizedException.ThrowIfNull(currentUserId);

        var customer = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
        NotFoundException.ThrowIfNull(customer);

        var seller = await _userRepository.GetByIdAsync(request.SellerId, cancellationToken);
        NotFoundException.ThrowIfNull(seller);

        var item = await _itemRepository.GetByIdAsync(request.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var conversation = await _conversationRepository.FirstOrDefaultAsync(x =>
            x.Item.Id == item.Id &&
            x.Members.Contains(customer) &&
            x.Members.Contains(seller),
            cancellationToken);

        return conversation != null;
    }
}