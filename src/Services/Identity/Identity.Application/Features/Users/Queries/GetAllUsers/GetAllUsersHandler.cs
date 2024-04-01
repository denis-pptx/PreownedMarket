using Identity.Application.Features.Users.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersHandler(UserManager<User> userManager, IMapper mapper)
    : IQueryHandler<GetAllUsersQuery, IEnumerable<UserVm>>
{
    public async Task<IEnumerable<UserVm>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        var userVms = new List<UserVm>();

        foreach (var user in users)
        {
            string? role = (await userManager.GetRolesAsync(user)).SingleOrDefault();

            var userVm = mapper.Map<UserVm>(user);
            userVm.Role = role ?? string.Empty;

            userVms.Add(userVm);
        }

        return userVms;
    }
}