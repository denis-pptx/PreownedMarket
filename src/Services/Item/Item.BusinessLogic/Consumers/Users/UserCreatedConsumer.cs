using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Repositories.UnitOfWork;
using MassTransit;
using Shared.Events.Users;

namespace Item.BusinessLogic.Consumers.Users;

public class UserCreatedConsumer(
    IUnitOfWork _unitOfWork, 
    IUserRepository _userRepository) 
    : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var user = new User()
        {
            Id = context.Message.Id
        };

        var existingUser = await _userRepository.GetByIdAsync(user.Id);

        if (existingUser is null)
        {
            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}