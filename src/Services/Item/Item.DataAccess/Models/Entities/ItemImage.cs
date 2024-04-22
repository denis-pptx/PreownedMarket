namespace Item.DataAccess.Models.Entities;

public class ItemImage : BaseEntity
{
    public string FilePath { get; set; } = string.Empty;
    public Guid ItemId { get; set; }
}
