namespace Item.DataAccess.Specifications.Implementations.Item;

public class ItemWithStatusSpecification : BaseSpecification<Models.Entities.Item>
{
    public ItemWithStatusSpecification(Guid id) : base(x => x.Id.Equals(id))
    {
        AddInclude(x => x.Status!);
    }
}