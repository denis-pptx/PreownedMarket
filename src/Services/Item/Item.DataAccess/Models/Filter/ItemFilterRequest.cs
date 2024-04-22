namespace Item.DataAccess.Models.Filter;

public class ItemFilterRequest
{
    public string? SearchTerm { get; set; }
    public Guid? CityId { get; set; }
    public string? CategoryNormalizedName { get; set; }
    public string? StatusNormalizedName { get; set; }
    public Guid? UserId { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}