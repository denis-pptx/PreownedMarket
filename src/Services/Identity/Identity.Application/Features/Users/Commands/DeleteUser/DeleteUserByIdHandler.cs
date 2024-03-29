
using Microsoft.AspNetCore.Http;

namespace Identity.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserByIdHandler(UserManager<User> userManager, IUserService userService) 
    : ICommandHandler<DeleteUserByIdCommand, Unit>
{

    public async Task<Unit> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var userIdentity = await userManager.FindByIdAsync(request.Id.ToString());
        if (userIdentity == null)
        {
            throw new NotFoundException();
        }

        var userActorId = userService.GetMyId();
        if (userActorId == userIdentity.Id) 
        {
            throw new ConflictException();
        }

        await userManager.DeleteAsync(userIdentity);

        return Unit.Value;
    }
}