namespace Item.DataAccess.Models.Entities;

public class Status : BaseEntity, IEquatable<Status>
{
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;

    public Status(string Name, string NormalizedName)
    {
        this.Name = Name;
        this.NormalizedName = NormalizedName;
    }

    public Status()
    {

    }

    public bool Equals(Status? other)
    {
        if (other is null)
        {
            return false;
        }

        return NormalizedName == other.NormalizedName;
    }
}