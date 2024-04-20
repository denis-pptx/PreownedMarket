﻿using Chat.Application.Abstractions.Contexts;
using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;
using Chat.Domain.Repositories;
using Identity.Application.Exceptions;

namespace Chat.Application.Features.Conversations.Queries.CheckConversationExistence;

public class CheckConversationExistenceQueryHandler(
    IUserContext _userContext,
    IUserRepository _userRepository,
    IItemRepository _itemRepository,
    IConversationRepository _conversationRepository)
    : ICommandHandler<CheckConversationExistenceQuery, CheckConversationExistenceResponse>
{
    public async Task<CheckConversationExistenceResponse> Handle(
        CheckConversationExistenceQuery query,
        CancellationToken cancellationToken)
    {
        var request = query.Request;

        var userId = _userContext.UserId;

        var customer = await _userRepository.GetByIdAsync(userId, cancellationToken);
        NotFoundException.ThrowIfNull(customer);

        var item = await _itemRepository.GetByIdAsync(request.ItemId, cancellationToken);
        NotFoundException.ThrowIfNull(item);

        var conversation = await _conversationRepository.FirstOrDefaultAsync(
            conversation => conversation.Item.Id == item.Id && conversation.Members.Contains(customer),
            cancellationToken);

        bool conversationExists = conversation != null;

        return new CheckConversationExistenceResponse(conversationExists);
    }
}