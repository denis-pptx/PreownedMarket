using Chat.Application.Models.Items;
using Chat.Domain.Entities;

namespace Chat.Application.Models.Conversations.Responses;

public record GetConversationWithLastMessageResponse(
    Guid ConversationId,
    Item Item,
    Message? LastMessage,
    IEnumerable<User> Users);