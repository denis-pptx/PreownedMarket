using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Messages.Requests;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;

namespace Chat.Application.Features.Messages.Commands.CreateMessage;

public record class CreateMessageCommand(CreateMessageRequest Request)
    : ICommand<MessageResponse>;