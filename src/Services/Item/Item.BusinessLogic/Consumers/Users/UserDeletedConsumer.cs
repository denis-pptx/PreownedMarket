using Contracts.Users;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using MassTransit;

namespace Item.BusinessLogic.Consumers.Users;

public class UserDeletedConsumer(IRepository<User> _userRepository) 
    : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var existingUser = await _userRepository.GetByIdAsync(context.Message.UserId);

        if (existingUser is not null)
        {
            await _userRepository.DeleteAsync(existingUser);
        }
    }
}