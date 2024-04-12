using Chat.Application.Data;
using Chat.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Contexts;

public class ApplicationDbContext 
    : MongoDbContext, IApplicationDbContext
{
    public IMongoCollection<Conversation> Conversations { get; set; }

    public IMongoCollection<Message> Messages { get; }

    public IMongoCollection<User> Users { get; }

    public IMongoCollection<Item> Items { get; }

    public ApplicationDbContext(IOptions<MongoDbOptions> dbOptions) 
        : base(dbOptions)
    {
        Conversations = Collection<Conversation>();
        Messages = Collection<Message>();
        Users = Collection<User>();
        Items = Collection<Item>();
    }

    public IMongoCollection<T> Collection<T>() => _mongoDatabase.GetCollection<T>(nameof(T));
}