namespace Chat.Infrastructure.Models;

public record MessageNotificationModel(
    string MessageId,
    DateTime CreatedAt,
    string SenderId,
    string ConversationId);