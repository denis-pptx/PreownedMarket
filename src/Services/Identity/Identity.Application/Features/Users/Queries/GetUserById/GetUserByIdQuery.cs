using Identity.Application.Features.Users.Queries.Models;

namespace Identity.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<UserVm>;
