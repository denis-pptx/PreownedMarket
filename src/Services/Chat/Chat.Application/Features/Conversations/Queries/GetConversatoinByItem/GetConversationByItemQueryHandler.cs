using AutoMapper;
using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Exceptions;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetConversatoinByItem;

public class GetConversationByItemQueryHandler(
    IMapper _mapper,
    IUserContext _userContext,
    IUserRepository _userRepository,
    IItemRepository _itemRepository, 
    IMessageRepository _messageRepository,
    IConversationRepository _conversationRepository) 
    : IQueryHandler<GetConversationByItemQuery, ConversationResponse>
{
    public async Task<ConversationResponse> Handle(
        GetConversationByItemQuery query, 
        CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(query.itemId, cancellationToken);
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
                conversation.Item.Id == item.Id && 
                conversation.Members.Contains(customer) && 
                conversation.Members.Contains(seller), 
            cancellationToken);

        NotFoundException.ThrowIfNull(conversation);

        var messages = await _messageRepository.GetByConversationIdAsync(conversation.Id, cancellationToken);
        var messagesResponse = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResponse>>(messages);

        var response = new ConversationResponse
        {
            ConversationId = conversation.Id,
            Item = conversation.Item,
            Members = conversation.Members,
            Messages = messagesResponse
        };

        return response;
    }
}