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
        Conversations = _mongoDatabase.GetCollection<Conversation>(nameof(Conversations));
        Messages = _mongoDatabase.GetCollection<Message>(nameof(Messages));
        Users = _mongoDatabase.GetCollection<User>(nameof(Users));
        Items = _mongoDatabase.GetCollection<Item>(nameof(Items));
    }
}