namespace Chat.Domain.Entities;

public class User : Entity, IEquatable<User>
{
    public string UserName { get; set; } = string.Empty;

    public bool Equals(User? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id;
    }
}