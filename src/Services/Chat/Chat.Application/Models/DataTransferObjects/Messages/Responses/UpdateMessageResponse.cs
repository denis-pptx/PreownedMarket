namespace Chat.Application.Models.DataTransferObjects.Messages.Responses;

public record UpdateMessageResponse(
    Guid MessageId,
    string Text,
    DateTime CreatedAt,
    Guid SenderId,
    Guid ConversationId);