namespace Item.BusinessLogic.Models.DTOs;

public class ItemDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double Price { get; set; }
    public Guid? CityId { get; set; }
    public Guid CategoryId { get; set; }
}
