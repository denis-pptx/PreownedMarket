using Chat.Application.Abstractions.Messaging;
using Chat.Application.Models.DataTransferObjects.Messages.Responses;

namespace Chat.Application.Features.Messages.Commands.DeleteMessage;

public record class DeleteMessageCommand(string MessageId) 
    : ICommand<DeleteMessageResponse>;