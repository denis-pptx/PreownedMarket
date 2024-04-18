using Chat.Domain.Entities;

namespace Chat.Application.Models.DataTransferObjects.Conversations.Responses;

public class GetConversationResponse
{
    public string ConversationId { get; set; } = string.Empty;
    public Item Item { get; set; } = default!;
    public IEnumerable<User> Members { get; set; } = [];
    public IEnumerable<Message> Messages { get; set; } = [];
}