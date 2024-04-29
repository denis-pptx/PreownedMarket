using Chat.Domain.Entities;

namespace Chat.Application.Models.DataTransferObjects.Conversations.Responses;

public record GetConversationWithLastMessageResponse(
    Guid ConversationId, 
    Item Item, 
    Message? LastMessage,
    IEnumerable<User> Users);