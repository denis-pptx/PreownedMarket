namespace Item.DataAccess.Specifications.Implementations.Item;

public class ItemWithImagesSpecification 
    : BaseSpecification<Models.Item>
{
    public ItemWithImagesSpecification(Guid id) 
        : base(x => x.Id == id)
    {
        AddInclude(x => x.Images);
    }
}