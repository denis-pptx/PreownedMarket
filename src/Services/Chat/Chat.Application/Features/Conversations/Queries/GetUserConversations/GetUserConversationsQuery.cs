using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;

namespace Chat.Application.Features.Conversations.Queries.GetUserConversations;

public record GetUserConversationsQuery() 
    : IQuery<IEnumerable<GetUserConversationResponse>>;