using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
using MassTransit;
using Shared.Events.Users;

namespace Item.BusinessLogic.Consumers.Users;

public class UserDeletedConsumer(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository) 
    : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var existingUser = await userRepository.GetByIdAsync(context.Message.UserId);

        if (existingUser is not null)
        {
            await userRepository.RemoveAsync(existingUser);

            await unitOfWork.SaveChangesAsync();
        }
    }
}