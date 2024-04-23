using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using MongoDB.Driver;

namespace Chat.Infrastructure.Repositories;

public class MongoRepository<TEntity>(IApplicationDbContext dbContext)
    where TEntity : Entity
{
    protected readonly IMongoCollection<TEntity> _collection = dbContext.Collection<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default)
    {
        return await _collection.Find(_ => true).ToListAsync(token);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _collection.Find(item => item.Id == id).FirstOrDefaultAsync(token);
    }

    public async Task AddAsync(TEntity entity, CancellationToken token = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: token);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        await _collection.ReplaceOneAsync(item => item.Id == entity.Id, entity, cancellationToken: token);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken token = default)
    {
        await _collection.DeleteOneAsync(item => item.Id == entity.Id, token);
    }
}