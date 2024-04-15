namespace Identity.Application.Features.Users.Commands.UpdateUserRole;

public record UpdateUserRoleCommand(UpdateUserRoleRequest Request) 
    : ICommand<Unit>;