namespace Chat.Application.Models.Messages.Requests;

public record CreateMessageRequest(
    string Text,
    Guid ConversationId);