using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;

namespace Chat.Infrastructure.Repositories;

public class ItemRepository(IApplicationDbContext dbContext)
    : MongoRepository<Item>(dbContext), IItemRepository
{

}