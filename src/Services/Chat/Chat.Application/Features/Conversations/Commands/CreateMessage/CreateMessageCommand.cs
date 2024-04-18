using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Conversations.Requests;
using MediatR;

namespace Chat.Application.Features.Conversations.Commands.CreateMessage;

public record class CreateMessageCommand(CreateMessageRequest Request) 
    : ICommand<Unit>;
