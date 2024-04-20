namespace Chat.Application.Models.DataTransferObjects.Messages.Responses;

public record UpdateMessageResponse(
    string MessageId,
    DateTime CreatedAt,
    string SenderId,
    string ConversationId);