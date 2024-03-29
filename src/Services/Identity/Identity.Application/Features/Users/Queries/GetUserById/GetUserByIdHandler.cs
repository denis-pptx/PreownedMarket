
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdHandler(UserManager<User> userManager) 
    : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id.ToString(), cancellationToken);

        if (user is null)
        {
            throw new NotFoundException();
        }

        return user;
    }
}
