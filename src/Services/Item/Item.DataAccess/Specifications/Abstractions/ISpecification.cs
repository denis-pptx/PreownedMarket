using Item.DataAccess.Models;
using System.Linq.Expressions;

namespace Item.DataAccess.Specifications.Abstractions;

public interface ISpecification<TEntity>
    where TEntity : BaseEntity
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
    List<Expression<Func<TEntity, object>>> Includes { get; }
    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDesc { get; }
}
