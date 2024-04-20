namespace Chat.Application.Models.DataTransferObjects.Messages.Responses;

public record CreateMessageResponse(
    string MessageId, 
    DateTime CreatedAt, 
    string SenderId, 
    string ConversationId);