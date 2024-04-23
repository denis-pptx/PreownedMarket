using Chat.Application.Abstractions.Messaging;
using MediatR;

namespace Chat.Application.Features.Conversations.Commands.DeleteConversation;

public record DeleteConversationCommand(Guid ConversationId) 
    : ICommand<Unit>;