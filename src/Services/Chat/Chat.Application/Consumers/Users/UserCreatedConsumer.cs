using Chat.Domain.Entities;
using Chat.Domain.Repositories;
using MassTransit;
using Shared.Events.Users;

namespace Chat.Application.Consumers.Users;

public class UserCreatedConsumer(IUserRepository _userRepository) 
    : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var user = new User()
        {
            Id = context.Message.Id,
            UserName = context.Message.UserName
        };

        var existingUser = await _userRepository.GetByIdAsync(user.Id);

        if (existingUser is null)
        {
            await _userRepository.AddAsync(user);
        }
    }
}