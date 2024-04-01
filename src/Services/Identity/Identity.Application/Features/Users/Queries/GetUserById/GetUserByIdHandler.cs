using Identity.Application.Features.Users.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdHandler(UserManager<User> _userManager, IMapper _mapper) 
    : IQueryHandler<GetUserByIdQuery, UserVm>
{
    public async Task<UserVm> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id.ToString(), cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        string? role = (await _userManager.GetRolesAsync(user)).SingleOrDefault();

        var userVm = _mapper.Map<User, UserVm>(user);
        userVm.Role = role ?? string.Empty;

        return userVm;
    }
}
