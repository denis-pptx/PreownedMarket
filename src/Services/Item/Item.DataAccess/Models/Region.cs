namespace Item.DataAccess.Models;

public class Region : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<City> Cities { get; set; } = [];
}
