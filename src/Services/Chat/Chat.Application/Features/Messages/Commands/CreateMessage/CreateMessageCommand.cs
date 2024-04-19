using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Messages.Requests;
using MediatR;

namespace Chat.Application.Features.Messages.Commands.CreateMessage;

public record class CreateMessageCommand(CreateMessageRequest Request)
    : ICommand<Unit>;
