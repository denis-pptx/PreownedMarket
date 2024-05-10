using Item.DataAccess.Models.Entities;

namespace Item.DataAccess.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);   
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task RemoveAsync(User user, CancellationToken cancellationToken = default);
}