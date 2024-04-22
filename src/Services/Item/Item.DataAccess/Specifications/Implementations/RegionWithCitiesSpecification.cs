using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Specifications.Implementations;

public class RegionWithCitiesSpecification : BaseSpecification<Region>
{
    public RegionWithCitiesSpecification()
    {
        AddInclude(x => x.Cities);
    }

    public RegionWithCitiesSpecification(Guid id) : base(x => x.Id.Equals(id)) 
    {
        AddInclude(x => x.Cities);
    }
}