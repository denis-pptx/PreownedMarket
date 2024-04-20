using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Requests;
using Chat.Application.Models.DataTransferObjects.Conversations.Responses;

namespace Chat.Application.Features.Conversations.Commands;

public record CreateConversationCommand(CreateConversationRequest Request) 
    : ICommand<CreateConversationResponse>;