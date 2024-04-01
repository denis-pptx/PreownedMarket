using Identity.Application.Features.Users.Queries.Models;

namespace Identity.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IQuery<IEnumerable<UserVm>>;
