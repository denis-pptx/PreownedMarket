namespace Item.DataAccess.Models;

public class User : BaseEntity
{
    public List<Item> Items { get; set; } = [];
}