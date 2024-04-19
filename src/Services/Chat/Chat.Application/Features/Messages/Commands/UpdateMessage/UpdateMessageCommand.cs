using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Messages.Requests;
using MediatR;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public record UpdateMessageCommand(
    string MessageId, 
    UpdateMessageRequest Request) 
    : ICommand<Unit>;