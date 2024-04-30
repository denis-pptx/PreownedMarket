using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetConversatoinByItem;

public class GetConversationByItemQueryHandler(
    IUserContext _userContext,
    IItemService _itemService,
    IUserRepository _userRepository, 
    IMessageRepository _messageRepository,
    IConversationRepository _conversationRepository) 
    : IQueryHandler<GetConversationByItemQuery, GetConversationResponse>
{
    public async Task<GetConversationResponse> Handle(
        GetConversationByItemQuery query, 
        CancellationToken cancellationToken)
    {
        var item = await _itemService.GetByIdAsync(query.itemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var userId = _userContext.UserId;

        var customer = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(customer);

        var seller = await _userRepository.GetByIdAsync(item.UserId, cancellationToken);
        NotFoundException.ThrowIfNull(seller);

        if (customer.Id == seller.Id)
        {
            throw new BadRequestException();
        }

        var conversation = await _conversationRepository.FirstOrDefaultAsync(
            conversation => 
                conversation.ItemId == item.Id && 
                conversation.MembersIds.Contains(customer.Id) && 
                conversation.MembersIds.Contains(seller.Id), 
            cancellationToken);

        NotFoundException.ThrowIfNull(conversation);

        var messages = await _messageRepository.GetByConversationIdAsync(conversation.Id, cancellationToken);

        var response = new GetConversationResponse
        {
            ConversationId = conversation.Id,
            Item = item,
            Members = [customer, seller],
            Messages = messages
        };

        return response;
    }
}