namespace Item.DataAccess.Models.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;

    public Category(string Name, string NormalizedName)
    {
        this.Name = Name;
        this.NormalizedName = NormalizedName;
    }

    public Category()
    {

    }
}