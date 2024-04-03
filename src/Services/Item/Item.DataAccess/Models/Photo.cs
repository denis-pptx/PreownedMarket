namespace Item.DataAccess.Models;

public class Photo : Entity
{
    public string Path { get; set; } = string.Empty;
    public int ItemId { get; set; }
}
