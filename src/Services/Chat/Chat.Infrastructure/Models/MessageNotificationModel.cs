namespace Chat.Infrastructure.Models;

public record MessageNotificationModel(
    Guid MessageId,
    string Text,
    DateTime CreatedAt,
    Guid SenderId,
    Guid ConversationId);