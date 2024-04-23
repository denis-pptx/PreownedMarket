namespace Chat.Application.Models.DataTransferObjects.Messages.Requests;

public record CreateMessageRequest(
    string Text,
    Guid ConversationId);