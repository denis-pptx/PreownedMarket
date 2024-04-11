using Chat.Domain.Entities;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data.Contexts;

public interface IApplicationDbContext
{
    IMongoCollection<Conversation> Conversations { get; }
    IMongoCollection<Message> Messages { get; }
    IMongoCollection<User> Users { get; }
    IMongoCollection<Item> Items { get; }
}