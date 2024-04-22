namespace Item.DataAccess.Models.Entities;

public class Item : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid? CityId { get; set; }
    public City? City { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public Guid StatusId { get; set; }
    public Status? Status { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public ICollection<ItemImage> Images { get; set; } = [];
}