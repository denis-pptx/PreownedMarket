namespace Item.DataAccess.Specifications.Implementations.Item;

public class ItemWithAllSpecification : BaseSpecification<Models.Entities.Item>
{
    public ItemWithAllSpecification(Guid id) : base(x => x.Id == id)
    {
        ConfigureIncludes();
    }
    public ItemWithAllSpecification()
    {
        ConfigureIncludes();
    }

    private void ConfigureIncludes()
    {
        AddInclude(x => x.City!);
        AddInclude(x => x.Category!);
        AddInclude(x => x.Status!);
        AddInclude(x => x.User!);
        AddInclude(x => x.Images);
    }
}
