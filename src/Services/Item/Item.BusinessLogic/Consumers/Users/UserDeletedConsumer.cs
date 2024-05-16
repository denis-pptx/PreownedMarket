using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
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
            await _userRepository.RemoveAsync(existingUser);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}