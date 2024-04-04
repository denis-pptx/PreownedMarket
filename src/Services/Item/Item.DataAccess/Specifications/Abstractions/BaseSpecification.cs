using Item.DataAccess.Models;
using System.Linq.Expressions;

namespace Item.DataAccess.Specifications.Abstractions;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : BaseEntity
{
    public BaseSpecification()
    {

    }

    public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>>? Criteria { get; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } = [];

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }

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
