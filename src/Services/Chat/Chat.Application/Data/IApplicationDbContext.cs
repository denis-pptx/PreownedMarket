using Chat.Domain.Entities;
using MongoDB.Driver;

namespace Chat.Application.Data;

public interface IApplicationDbContext
{
    IMongoCollection<Conversation> Conversations { get; }
    IMongoCollection<Message> Messages { get; }
    IMongoCollection<User> Users { get; }
    IMongoCollection<Item> Items { get; }
    IMongoCollection<T> Collection<T>();
}