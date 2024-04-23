using Chat.Application.Models.DataTransferObjects.Messages.Responses;
using Chat.Domain.Entities;

namespace Chat.Application.Models.DataTransferObjects.Conversations.Responses;

public class GetConversationResponse
{
    public Guid ConversationId { get; set; }
    public Item Item { get; set; } = default!;
    public IEnumerable<User> Members { get; set; } = [];
    public IEnumerable<MessageResponse> Messages { get; set; } = [];
}