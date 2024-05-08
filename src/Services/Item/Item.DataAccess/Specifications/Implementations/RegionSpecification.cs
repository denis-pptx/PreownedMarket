using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Specifications.Implementations;

public class RegionSpecification : BaseSpecification<Region>
{
    public RegionSpecification()
    {
        AddInclude(x => x.Cities);
    }

    public RegionSpecification(Guid id) : base(x => x.Id.Equals(id))
    {
        AddInclude(x => x.Cities);
    }
}