using Chat.Domain.Entities;

namespace Chat.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken token = default);
}