namespace Item.DataAccess.Models;

public class Item : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double? Price { get; set; }
    public DateTime CreatedAt { get; set; }

    public int? CityId { get; set; }
    public int CategoryId {  get; set; }
    public int StatusId {  get; set; }
    public Guid UserId {  get; set; }
}
