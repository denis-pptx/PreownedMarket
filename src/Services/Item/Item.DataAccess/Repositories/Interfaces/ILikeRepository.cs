using Item.DataAccess.Models.Entities;
using Item.DataAccess.Specifications.Interfaces;

namespace Item.DataAccess.Repositories.Interfaces;

using Item = Models.Entities.Item;

public interface ILikeRepository : IRepository<Like>
{
    Task<IEnumerable<Item>> GetByUserIdAsync(Guid userId, ISpecification<Item> itemSpecification, CancellationToken token = default);
}
