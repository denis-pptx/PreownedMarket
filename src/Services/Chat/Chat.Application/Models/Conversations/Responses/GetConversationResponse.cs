using Chat.Application.Models.Items;
using Chat.Domain.Entities;

namespace Chat.Application.Models.Conversations.Responses;

public record GetConversationResponse(
    Guid ConversationId,
    Item Item,
    IEnumerable<User> Members,
    IEnumerable<Message> Messages);