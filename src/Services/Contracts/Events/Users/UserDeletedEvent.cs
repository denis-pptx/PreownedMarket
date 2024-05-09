namespace Shared.Events.Users;

public class UserDeletedEvent
{
    public Guid UserId { get; set; }
}