
using Microsoft.AspNetCore.Http;

namespace Identity.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserByIdHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) 
    : ICommandHandler<DeleteUserByIdCommand, Unit>
{
    private readonly ClaimsPrincipal? _userActor = httpContextAccessor.HttpContext?.User;

    public async Task<Unit> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var userIdentity = await userManager.FindByIdAsync(request.Id.ToString());
        if (userIdentity == null)
        {
            throw new NotFoundException();
        }

        var userActorId = _userActor?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userActorId == userIdentity.Id) 
        {
            throw new ConflictException();
        }

        await userManager.DeleteAsync(userIdentity);

        return Unit.Value;
    }
}