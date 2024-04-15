using Identity.Application.Models.DataTransferObjects.Users.Responses;

namespace Identity.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<GetUserResponse>;