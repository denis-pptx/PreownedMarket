using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
using MassTransit;
using Shared.Events.Users;

namespace Item.BusinessLogic.Consumers.Users;

public class UserCreatedConsumer(
    IUnitOfWork unitOfWork, 
    IUserRepository userRepository) 
    : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var user = new User()
        {
            Id = context.Message.Id
        };

        var existingUser = await userRepository.GetByIdAsync(user.Id);

        if (existingUser is null)
        {
            await userRepository.AddAsync(user);

            await unitOfWork.SaveChangesAsync();
        }
    }
}