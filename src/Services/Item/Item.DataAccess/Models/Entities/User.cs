namespace Item.DataAccess.Models.Entities;

public class User : BaseEntity
{
    public List<Item> Items { get; set; } = [];
}