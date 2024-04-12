using Chat.Domain.Entities;

namespace Chat.Application.Data;

public interface IMongoRepository<T> 
    where T : Entity
{
    Task<List<T>> GetAllAsync(CancellationToken token = default);
    Task<T> GetByIdAsync(string id, CancellationToken token = default);
    Task AddAsync(T entity, CancellationToken token = default);
    Task UpdateAsync(string id, T entity, CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
}