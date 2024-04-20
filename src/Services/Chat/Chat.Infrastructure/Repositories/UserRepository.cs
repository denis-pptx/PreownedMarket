using Chat.Application.Abstractions.Contexts;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;

namespace Chat.Infrastructure.Repositories;

public class UserRepository(IApplicationDbContext dbContext)
    : MongoRepository<User>(dbContext), IUserRepository
{

}