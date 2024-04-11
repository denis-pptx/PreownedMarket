namespace Chat.Domain.Entities;

public class Conversation : Entity
{
    public Item Item { get; set; } = default!;
    public Message? LastMessage { get; set; }
    public IEnumerable<User> Members { get; set; } = [];
}