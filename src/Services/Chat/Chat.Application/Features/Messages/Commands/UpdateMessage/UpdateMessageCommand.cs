using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.Messages.Requests;
using Chat.Domain.Entities;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public record UpdateMessageCommand(Guid MessageId, UpdateMessageRequest Request) 
    : ICommand<Message>;