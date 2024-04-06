namespace Item.DataAccess.Models;

public class Photo : BaseEntity
{
    public string Path { get; set; } = string.Empty;
    public int ItemId { get; set; }
}