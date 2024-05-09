using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Repositories.UnitOfWork;
using MassTransit;
using Shared.Events.Users;

namespace Item.BusinessLogic.Consumers.Users;

public class UserDeletedConsumer(
    IUnitOfWork _unitOfWork,
    IUserRepository _userRepository) 
    : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var existingUser = await _userRepository.GetByIdAsync(context.Message.UserId);

        if (existingUser is not null)
        {
            _userRepository.Remove(existingUser);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}