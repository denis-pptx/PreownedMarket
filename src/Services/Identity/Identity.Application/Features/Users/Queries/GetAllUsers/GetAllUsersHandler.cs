namespace Identity.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersHandler(UserManager<User> _userManager, IMapper _mapper)
    : IQueryHandler<GetAllUsersQuery, IEnumerable<UserVm>>
{
    public async Task<IEnumerable<UserVm>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync(cancellationToken);
        var userVms = new List<UserVm>();

        foreach (var user in users)
        {
            string? role = (await _userManager.GetRolesAsync(user)).SingleOrDefault();

            var userVm = _mapper.Map<UserVm>(user);
            userVm.Role = role ?? string.Empty;

            userVms.Add(userVm);
        }

        return userVms;
    }
}