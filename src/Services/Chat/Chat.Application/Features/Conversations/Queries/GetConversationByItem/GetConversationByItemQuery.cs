using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Conversations.Responses;

namespace Chat.Application.Features.Conversations.Queries.GetConversationByItem;

public record GetConversationByItemQuery(Guid ItemId) 
    : IQuery<GetConversationResponse>;