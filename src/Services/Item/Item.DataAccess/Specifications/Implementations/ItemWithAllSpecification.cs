namespace Item.DataAccess.Specifications.Implementations;

public class ItemWithAllSpecification : BaseSpecification<Models.Item>
{
    public ItemWithAllSpecification(Guid id) : base(x => x.Id == id)
    {
        AddInclude(x => x.City!);
        AddInclude(x => x.Category!);
        AddInclude(x => x.Status!);
        AddInclude(x => x.User!);
    }
    public ItemWithAllSpecification()
    {
        AddInclude(x => x.City!);
        AddInclude(x => x.Category!);
        AddInclude(x => x.Status!);
        AddInclude(x => x.User!);
    }
}
