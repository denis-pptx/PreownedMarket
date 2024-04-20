using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Messages.Requests;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;

namespace Chat.Application.Features.Messages.Commands.UpdateMessage;

public record UpdateMessageCommand(Guid MessageId, UpdateMessageRequest Request) 
    : ICommand<UpdateMessageResponse>;