using Chat.Domain.Entities;

namespace Chat.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id, CancellationToken token = default);
}