using Item.DataAccess.Models.Entities;
using Item.DataAccess.Specifications.Interfaces;
using System.Linq.Expressions;

namespace Item.DataAccess.Specifications.Implementations;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }

    public BaseSpecification()
    {

    }

    public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void SetOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void SetOrderByDesc(Expression<Func<TEntity, object>> orderByDescExpression)
    {
        OrderByDesc = orderByDescExpression;
    }
}