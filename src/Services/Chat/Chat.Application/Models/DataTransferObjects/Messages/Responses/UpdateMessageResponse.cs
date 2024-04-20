namespace Chat.Application.Models.DataTransferObjects.Messages.Responses;

public record UpdateMessageResponse(
    Guid MessageId,
    DateTime CreatedAt,
    Guid SenderId,
    Guid ConversationId);