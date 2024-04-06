namespace Item.DataAccess.Models;

public class Status : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string NormalizedName {  get; set; } = string.Empty;

    public Status(string Name, string NormalizedName)
    {
        this.Name = Name;
        this.NormalizedName = NormalizedName;
    }

    public Status()
    {
        
    }
}