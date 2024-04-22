using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;

namespace Chat.Application.Models.DataTransferObjects.Conversations.Responses;

public record ConversationWithLastMessageResponse(
    Guid ConversationId, 
    Item Item, 
    MessageResponse? LastMessage,
    IEnumerable<User> Users);