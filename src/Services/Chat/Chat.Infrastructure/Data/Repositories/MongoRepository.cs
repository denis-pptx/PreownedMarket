using Chat.Application.Data;
using Chat.Domain.Entities;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Repositories;

public class MongoRepository<T>(IApplicationDbContext dbContext) 
    : IMongoRepository<T> where T : Entity
{
    private readonly IMongoCollection<T> _collection = dbContext.Collection<T>();

    public async Task<List<T>> GetAllAsync(CancellationToken token = default)
    {
        return await _collection.Find(_ => true).ToListAsync(token);
    }

    public async Task<T> GetByIdAsync(string id, CancellationToken token = default)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(token);
    }

    public async Task AddAsync(T entity, CancellationToken token = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: token);
    }

    public async Task UpdateAsync(string id, T entity, CancellationToken token = default)
    {
        await _collection.ReplaceOneAsync(x => x.Id == id, entity, cancellationToken: token);
    }

    public async Task DeleteAsync(string id, CancellationToken token = default)
    {
        await _collection.DeleteOneAsync(x => x.Id == id, token);
    }
}
