using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Conversations.Responses;

namespace Chat.Application.Features.Conversations.Queries.GetConversation;

public record GetConversationQuery(Guid ConversationId) 
    : IQuery<GetConversationResponse>;