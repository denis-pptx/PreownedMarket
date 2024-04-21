namespace Chat.Application.Models.DataTransferObjects.Messages.Responses;

public record MessageResponse(
    Guid MessageId,
    string Text,
    DateTime CreatedAt,
    Guid SenderId,
    Guid ConversationId);