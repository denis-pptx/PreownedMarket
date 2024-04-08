using Item.DataAccess.Models;
using Item.DataAccess.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Specifications.Common;

public static class SpecificationQueryBuilder
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(
        this IQueryable<TEntity> inputQuery, 
        ISpecification<TEntity> specification) 
        where TEntity : BaseEntity
    {
        var query = inputQuery;

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, 
            (current, include) => current.Include(include));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        } 
        else if (specification.OrderByDesc != null)
        {
            query = query.OrderByDescending(specification.OrderByDesc);
        }

        return query;
    }
}