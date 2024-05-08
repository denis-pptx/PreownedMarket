using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using Chat.Infrastructure.Options.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.Infrastructure.Data;

public class ApplicationDbContext(IOptions<MongoDbOptions> options)
    : MongoDbContext(options), IApplicationDbContext
{
    public IMongoCollection<Conversation> Conversations => Collection<Conversation>();

    public IMongoCollection<Message> Messages => Collection<Message>();

    public IMongoCollection<User> Users => Collection<User>();
}