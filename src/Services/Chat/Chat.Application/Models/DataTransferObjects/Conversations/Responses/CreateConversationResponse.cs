using Chat.Domain.Entities;

namespace Chat.Application.Models.DataTransferObjects.Conversations.Responses;

public record CreateConversationResponse(
    Guid ConversationId, 
    Item Item, 
    IEnumerable<User> Members);