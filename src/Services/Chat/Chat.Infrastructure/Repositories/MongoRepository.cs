using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using MongoDB.Driver;

namespace Chat.Infrastructure.Repositories;

public class MongoRepository<TEntity>(IApplicationDbContext dbContext)
    where TEntity : Entity
{
    protected readonly IMongoCollection<TEntity> Collection = dbContext.Collection<TEntity>();
    protected readonly IApplicationDbContext DbContext = dbContext;
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default)
    {
        return await Collection.Find(_ => true).ToListAsync(token);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await Collection.Find(item => item.Id == id).FirstOrDefaultAsync(token);
    }

    public async Task AddAsync(TEntity entity, CancellationToken token = default)
    {
        await Collection.InsertOneAsync(entity, cancellationToken: token);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        await Collection.ReplaceOneAsync(item => item.Id == entity.Id, entity, cancellationToken: token);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken token = default)
    {
        await Collection.DeleteOneAsync(item => item.Id == entity.Id, token);
    }
}