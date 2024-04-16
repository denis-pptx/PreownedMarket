using Chat.Application.Abstractions.Messaging;
using Chat.Domain.Entities;

namespace Chat.Application.Features.Conversations.Queries.GetUserConversations;

public record GetUserConversationsQuery() 
    : IQuery<IEnumerable<Conversation>>;