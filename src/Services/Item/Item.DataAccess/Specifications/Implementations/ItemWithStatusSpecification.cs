﻿namespace Item.DataAccess.Specifications.Implementations;

public class ItemWithStatusSpecification : BaseSpecification<Models.Item>
{
    public ItemWithStatusSpecification(Guid id) : base(x => x.Id.Equals(id))
    {
        AddInclude(x => x.Status!);
    }
}