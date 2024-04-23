using Chat.Infrastructure.Options.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.Infrastructure.Contexts;

public abstract class MongoDbContext
{
    protected readonly IMongoClient _mongoClient;
    protected readonly IMongoDatabase _mongoDatabase;

    public MongoDbContext(IOptions<MongoDbOptions> dbOptions)
    {
        var options = dbOptions.Value;

        _mongoClient = new MongoClient(options.ConnectionString);
        _mongoDatabase = _mongoClient.GetDatabase(options.DatabaseName);
    }

    public IMongoCollection<T> Collection<T>() => _mongoDatabase.GetCollection<T>(typeof(T).Name);
}