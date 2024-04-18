using Chat.Domain.Entities;
using System.Linq.Expressions;

namespace Chat.Domain.Repositories;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(string id, CancellationToken token = default);
}
