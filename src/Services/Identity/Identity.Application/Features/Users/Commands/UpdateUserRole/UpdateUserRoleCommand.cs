namespace Identity.Application.Features.Users.Commands.UpdateUserRole;

public record UpdateUserRoleCommand(Guid UserId, string NewRole) 
    : ICommand<Unit>;