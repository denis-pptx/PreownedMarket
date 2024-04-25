using Contracts.Users;
using MassTransit;

namespace Identity.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserByIdHandler(
    UserManager<User> _userManager, 
    ICurrentUserService _userService, 
    IPublishEndpoint _publishEndpoint) 
    : ICommandHandler<DeleteUserByIdCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var userIdentity = await _userManager.FindByIdAsync(request.Id.ToString());

        if (userIdentity is null)
        {
            throw new NotFoundException(UserErrorMessages.NotFound);
        }

        var userActorId = _userService.UserId;

        if (userActorId == userIdentity.Id)
        {
            throw new ConflictException(UserErrorMessages.DeleteYourself);
        }

        await _userManager.DeleteAsync(userIdentity);

        await _publishEndpoint.Publish(new UserDeletedEvent 
            { 
                UserId = Guid.Parse(userIdentity.Id) 
            }, 
            cancellationToken);

        return Unit.Value;
    }
}