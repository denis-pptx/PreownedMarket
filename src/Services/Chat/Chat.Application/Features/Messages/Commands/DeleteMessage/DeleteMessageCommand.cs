using Chat.Application.Abstractions.Messaging;
using Chat.Domain.Entities;

namespace Chat.Application.Features.Messages.Commands.DeleteMessage;

public record class DeleteMessageCommand(Guid MessageId) 
    : ICommand<Message>;