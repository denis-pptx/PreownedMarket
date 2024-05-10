namespace Shared.Events.Users;

public class UserCreatedEvent
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
}