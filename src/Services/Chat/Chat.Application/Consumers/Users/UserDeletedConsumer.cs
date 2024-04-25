using Chat.Domain.Repositories;
using Contracts.Users;
using MassTransit;

namespace Chat.Application.Consumers.Users;

public class UserDeletedConsumer(IUserRepository _userRepository) 
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