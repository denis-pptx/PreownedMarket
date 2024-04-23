using Chat.Domain.Entities;

namespace Chat.Infrastructure.Models.Conversations;

public record ConversationCreatedNotification(
    Guid ConversationId,
    Item Item,
    IEnumerable<User> Members);