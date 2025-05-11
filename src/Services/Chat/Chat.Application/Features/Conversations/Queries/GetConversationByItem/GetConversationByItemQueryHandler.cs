using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Grpc;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Conversations.Responses;
using Chat.Domain.Repositories;
using Shared.Errors.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.GetConversationByItem;

public class GetConversationByItemQueryHandler(
    IUserContext userContext,
    IItemService itemService,
    IUserRepository userRepository, 
    IMessageRepository messageRepository,
    IConversationRepository conversationRepository) 
    : IQueryHandler<GetConversationByItemQuery, GetConversationResponse>
{
    public async Task<GetConversationResponse> Handle(
        GetConversationByItemQuery query, 
        CancellationToken cancellationToken)
    {
        var item = await itemService.GetByIdAsync(query.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var userId = userContext.UserId;

        var customer = await userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(customer);

        var seller = await userRepository.GetByIdAsync(item.UserId, cancellationToken);
        NotFoundException.ThrowIfNull(seller);

        if (customer.Id == seller.Id)
        {
            throw new BadRequestException();
        }

        var conversation = await conversationRepository.FirstOrDefaultAsync(
            conversation => 
                conversation.ItemId == item.Id && 
                conversation.MembersIds.Contains(customer.Id) && 
                conversation.MembersIds.Contains(seller.Id), 
            cancellationToken);

        NotFoundException.ThrowIfNull(conversation);

        var messages = await messageRepository.GetByConversationIdAsync(conversation.Id, cancellationToken);

        return new GetConversationResponse(
            conversation.Id,
            item,
            [customer, seller],
            messages);
    }
}