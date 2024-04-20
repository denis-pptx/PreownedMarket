namespace Chat.Infrastructure.Models;

public record MessageNotificationModel(
    Guid MessageId,
    DateTime CreatedAt,
    Guid SenderId,
    Guid ConversationId);