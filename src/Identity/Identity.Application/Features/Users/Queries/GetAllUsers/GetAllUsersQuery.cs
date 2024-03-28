namespace Identity.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IQuery<IEnumerable<User>>;
