using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Conversations.Responses;

namespace Chat.Application.Features.Conversations.Queries.GetUserConversations;

public record GetAllUserConversationsQuery() 
    : IQuery<IEnumerable<GetConversationWithLastMessageResponse>>;