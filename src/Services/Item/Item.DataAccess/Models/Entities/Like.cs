namespace Item.DataAccess.Models.Entities;

public class Like : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }
    public DateTime CreatedOn { get; set; }
}