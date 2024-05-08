namespace Item.DataAccess.Specifications.Implementations;

public class ItemSpecification 
    : BaseSpecification<Models.Entities.Item>
{
    public ItemSpecification(Guid id) 
        : base(x => x.Id == id)
    {
        ConfigureIncludes();
    }
    public ItemSpecification()
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
