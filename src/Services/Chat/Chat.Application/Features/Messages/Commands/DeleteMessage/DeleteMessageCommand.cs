using Chat.Application.Abstractions.Messaging;
using MediatR;

namespace Chat.Application.Features.Messages.Commands.DeleteMessage;

public record class DeleteMessageCommand(string MessageId) 
    : ICommand<Unit>;