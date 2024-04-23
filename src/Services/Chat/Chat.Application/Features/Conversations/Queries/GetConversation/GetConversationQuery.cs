using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;

namespace Chat.Application.Features.Conversations.Queries.GetConversation;

public record GetConversationQuery(Guid ConversationId) 
    : IQuery<GetConversationResponse>;