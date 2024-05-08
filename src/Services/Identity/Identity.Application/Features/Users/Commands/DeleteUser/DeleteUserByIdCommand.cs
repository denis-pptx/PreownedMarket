using Identity.Application.Abstractions.Messaging;
using MediatR;

namespace Identity.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserByIdCommand(Guid Id) : ICommand<Unit>;