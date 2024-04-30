using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Conversations.Requests;
using Chat.Application.Models.Conversations.Responses;

namespace Chat.Application.Features.Conversations.Commands.CreateConversation;

public record CreateConversationCommand(CreateConversationRequest Request)
    : ICommand<CreateConversationResponse>;