namespace Item.DataAccess.Models.Entities;

public class City : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid RegionId { get; set; }
    public Region? Region { get; set; }
}