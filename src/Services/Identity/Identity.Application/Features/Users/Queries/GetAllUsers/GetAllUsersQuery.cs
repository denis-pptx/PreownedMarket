using Identity.Application.Abstractions.Messaging;
using Identity.Application.Models.DataTransferObjects.Users.Responses;

namespace Identity.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IQuery<IEnumerable<GetUserResponse>>;