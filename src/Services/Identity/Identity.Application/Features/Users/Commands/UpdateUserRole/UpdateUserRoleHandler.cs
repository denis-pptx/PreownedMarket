
namespace Identity.Application.Features.Users.Commands.UpdateUserRole;

public class UpdateUserRoleHandler(
    UserManager<User> _userManager, 
    RoleManager<IdentityRole> _roleManager, 
    ICurrentUserService _userSerivce) 
    : ICommandHandler<UpdateUserRoleCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userActorId = _userSerivce.UserId;
        if (userActorId == request.UserId.ToString())
        {
            throw new ConflictException("It is not possible to update the role for yourself");
        }

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        string? currentRole = (await _userManager.GetRolesAsync(user)).SingleOrDefault();
        string? newRole = (await _roleManager.FindByNameAsync(request.NewRole))?.Name;

        if (newRole is null)
        {
            throw new NotFoundException("Role not found");
        } 
        else
        {
            if (currentRole is not null)
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole);
            }
            
            await _userManager.AddToRoleAsync(user, newRole);

            return Unit.Value;
        }
    }
}
