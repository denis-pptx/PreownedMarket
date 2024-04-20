namespace Chat.Application.Models.DataTransferObjects.Messages.Responses;

public record CreateMessageResponse(
    Guid MessageId, 
    DateTime CreatedAt,
    Guid SenderId,
    Guid ConversationId);