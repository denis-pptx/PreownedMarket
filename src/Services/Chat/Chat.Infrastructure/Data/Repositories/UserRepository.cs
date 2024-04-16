using Chat.Application.Data;
using Chat.Domain.Entities;
using Chat.Domain.Repositories;

namespace Chat.Infrastructure.Data.Repositories;

public class UserRepository(IApplicationDbContext dbContext) 
    : MongoRepository<User>(dbContext), IUserRepository
{

}