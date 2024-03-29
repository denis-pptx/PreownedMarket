
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersHandler(UserManager<User> userManager) 
    : IQueryHandler<GetAllUsersQuery, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await userManager.Users.ToListAsync(cancellationToken);
    }
}
