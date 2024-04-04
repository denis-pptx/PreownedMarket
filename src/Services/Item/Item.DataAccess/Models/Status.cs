namespace Item.DataAccess.Models;

public class Status : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string NormalizedName {  get; set; } = string.Empty;
}
