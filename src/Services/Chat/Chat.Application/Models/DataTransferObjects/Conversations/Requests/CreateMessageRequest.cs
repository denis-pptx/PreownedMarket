namespace Chat.Application.Models.DataTransferObjects.Conversations.Requests;

public record CreateMessageRequest(
    string Text,
    string ConversationId);