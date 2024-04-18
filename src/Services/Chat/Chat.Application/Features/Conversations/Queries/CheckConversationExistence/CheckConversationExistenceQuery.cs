using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Requests;

namespace Chat.Application.Features.Conversations.Queries.CheckConversationExistence;

public record CheckConversationExistenceQuery(CheckConversationExistenceRequest Request)
    : ICommand<bool>;