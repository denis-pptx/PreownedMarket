
namespace Identity.Application.Features.Users.Commands.UpdateUserRole;

public class UpdateUserRoleHandler(
    UserManager<User> userManager, 
    RoleManager<IdentityRole> roleManager, 
    IUserService userSerivce) 
    : ICommandHandler<UpdateUserRoleCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userActorId = userSerivce.GetMyId();
        if (userActorId == request.UserId.ToString())
        {
            throw new ConflictException("It is not possible to update the role for yourself");
        }

        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        string? currentRole = (await userManager.GetRolesAsync(user)).SingleOrDefault();
        string? newRole = (await roleManager.FindByNameAsync(request.NewRole))?.Name;

        if (newRole is null)
        {
            throw new NotFoundException("Role not found");
        } 
        else
        {
            if (currentRole is not null)
            {
                await userManager.RemoveFromRoleAsync(user, currentRole);
            }
            
            await userManager.AddToRoleAsync(user, newRole);

            return Unit.Value;
        }
    }
}
