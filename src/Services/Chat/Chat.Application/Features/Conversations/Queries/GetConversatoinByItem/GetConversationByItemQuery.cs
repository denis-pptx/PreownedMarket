using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;

namespace Chat.Application.Features.Conversations.Queries.GetConversatoinByItem;

public record GetConversationByItemQuery(Guid itemId) 
    : IQuery<GetConversationResponse>;