namespace Chat.Domain.Entities;

public class Conversation : Entity
{
    public Item Item { get; set; } = default!;
    public IEnumerable<User> Members { get; set; } = [];
}