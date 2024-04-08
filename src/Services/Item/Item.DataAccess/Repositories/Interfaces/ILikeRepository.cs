using Item.DataAccess.Models;
using Item.DataAccess.Specifications.Interfaces;

namespace Item.DataAccess.Repositories.Interfaces;

using Item = Models.Item;

public interface ILikeRepository : IRepository<Like>
{
    Task<IEnumerable<Item>> GetByUserIdAsync(Guid userId, ISpecification<Item> itemSpecification, CancellationToken token = default);
}
