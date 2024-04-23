namespace Chat.Infrastructure.Models.Messages;

public record MessageNotification(
    Guid MessageId,
    string Text,
    DateTime CreatedAt,
    Guid SenderId,
    Guid ConversationId);