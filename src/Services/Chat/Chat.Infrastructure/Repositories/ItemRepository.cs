using Chat.Application.Data;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Chat.Infrastructure.Repositories;

public class ItemRepository(IApplicationDbContext dbContext)
    : MongoRepository<Item>(dbContext), IItemRepository
{

}