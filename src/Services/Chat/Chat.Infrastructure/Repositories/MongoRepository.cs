using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using MongoDB.Driver;

namespace Chat.Infrastructure.Repositories;

public class MongoRepository<T>(IApplicationDbContext dbContext)
    where T : Entity
{
    protected readonly IMongoCollection<T> _collection = dbContext.Collection<T>();

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
    {
        return await _collection.Find(_ => true).ToListAsync(token);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(token);
    }

    public async Task AddAsync(T entity, CancellationToken token = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: token);
    }

    public async Task UpdateAsync(T entity, CancellationToken token = default)
    {
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: token);
    }

    public async Task DeleteAsync(T entity, CancellationToken token = default)
    {
        await _collection.DeleteOneAsync(x => x.Id == entity.Id, token);
    }
}